﻿using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Application.Orders.Notifications.OrderAdded;
using Cocorico.Application.Orders.Queries.CalculatePrice;
using Cocorico.Application.Orders.Queries.CanAddOrder;
using Cocorico.Application.Orders.Services.RotatingId;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Dtos.Ingredient;
using Cocorico.Shared.Dtos.Order;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Cocorico.Shared.Entities;
using Cocorico.Shared.Exceptions;

namespace Cocorico.Application.Orders.Commands.AddOrder
{
    public sealed class AddOrderCommandHandler : CommandHandlerBase<AddOrderCommand>
    {
        private readonly IOrderRotatingIdService _idService;

        public AddOrderCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context,
            IOrderRotatingIdService idService)
            : base(mediator, mapper, context) =>
            _idService = idService;

        protected override async Task Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            // TODO: date service
            var dateAdded = DateTime.Now;
            var canAdd = await Mediator.Send(new CanAddOrderQuery(dateAdded), cancellationToken);
            if (!canAdd) throw new StoreClosedException();

            var sandwichesInDb = await Context
                .Sandwiches
                .ToListAsync(cancellationToken);

            var sandwichesFromOrderInDb = request.Dto.Sandwiches
                .Select(sandwichDto => sandwichesInDb.SingleOrDefault(s => s.Id == sandwichDto.Id))
                .Where(s => !(s is null))
                .ToList();

            if (sandwichesFromOrderInDb.Count == 0) throw new EntityNotFoundException();

            var orderPrice = await Mediator.Send(new CalculatePriceQuery(request.Dto), cancellationToken);

            var currentOpening = await Context.Openings
                .FirstAsync(o => o.Start <= dateAdded && o.End > dateAdded, cancellationToken);

            var newOrder = new Order
            {
                CocoricoUserId = request.Dto.CustomerId,
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
                        request.Dto.SandwichModifications.SingleOrDefault(kvp => kvp.Key.Id == sandwich.Id).Value
                        ?? new List<IngredientModificationDto>()),
                });
            }

            Context.Orders.Add(newOrder);

            await Context.SaveChangesAsync(cancellationToken);

            await Mediator.Publish(new OrderAddedEvent(Mapper.Map<WorkerOrderViewDto>(newOrder)), cancellationToken);
        }
    }
}