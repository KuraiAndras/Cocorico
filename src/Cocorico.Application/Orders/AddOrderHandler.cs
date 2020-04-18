using AutoMapper;
using Cocorico.Application.Orders.Notifications;
using Cocorico.Application.Orders.Services.RotatingId;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Dtos.Ingredients;
using Cocorico.Shared.Entities;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders
{
    public sealed class AddOrderHandler : HandlerBase<AddOrder>
    {
        private readonly IOrderRotatingIdService _idService;

        public AddOrderHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context,
            IOrderRotatingIdService idService)
            : base(mediator, mapper, context) =>
            _idService = idService;

        protected override async Task Handle(AddOrder request, CancellationToken cancellationToken)
        {
            var dateAdded = DateTime.Now;
            var canAdd = await Mediator.Send(new CanAddOrder { RequestTime = dateAdded }, cancellationToken);
            if (!canAdd) throw new StoreClosedException();

            var sandwichesInDb = await Context
                .Sandwiches
                .ToListAsync(cancellationToken);

            var sandwichesFromOrderInDb = request.Sandwiches
                .Select(sandwichDto => sandwichesInDb.SingleOrDefault(s => s.Id == sandwichDto.Id))
                .Where(s => !(s is null))
                .ToList();

            if (sandwichesFromOrderInDb.Count == 0) throw new EntityNotFoundException();

            var orderPrice = await Mediator.Send(Mapper.Map<CalculatePrice>(request), cancellationToken);

            var currentOpening = await Context.Openings
                .FirstAsync(o => o.Start <= dateAdded && o.End > dateAdded, cancellationToken);

            var newOrder = new Order
            {
                CocoricoUserId = request.CustomerId,
                Price = orderPrice,
                State = OrderState.OrderPlaced,
                RotatingId = _idService.GetNextId(),
                Time = dateAdded,
                Opening = currentOpening,
            };

            foreach (var sandwich in sandwichesFromOrderInDb)
            {
                newOrder.SandwichOrders.Add(new SandwichOrder
                {
                    Order = newOrder,
                    Sandwich = sandwich,
                    IngredientModifications = Mapper.Map<ICollection<IngredientModification>>(
                        request.SandwichModifications.SingleOrDefault(kvp => kvp.Key.Id == sandwich.Id).Value
                        ?? new List<IngredientModificationDto>()),
                });
            }

            Context.Orders.Add(newOrder);

            await Context.SaveChangesAsync(cancellationToken);

            await Mediator.Publish(new OrderAddedEvent(Mapper.Map<WorkerOrderViewDto>(newOrder)), cancellationToken);
        }
    }
}
