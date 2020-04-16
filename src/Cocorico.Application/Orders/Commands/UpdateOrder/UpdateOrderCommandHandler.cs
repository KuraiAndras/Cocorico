using AutoMapper;
using Cocorico.Application.Orders.Notifications.OrderAdded;
using Cocorico.Persistence;
using Cocorico.Shared.Dtos.Orders;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommandHandler : CommandHandlerBase<UpdateOrderCommand>
    {
        public UpdateOrderCommandHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await Context.Orders
                            .SingleOrDefaultAsync(o => o.Id == request.Dto.OrderId, cancellationToken)
                        ?? throw new EntityNotFoundException($"Order not found with id:{request.Dto.OrderId}");

            order.State = request.Dto.State;

            Context.Orders.Update(order);

            await Context.SaveChangesAsync(cancellationToken);

            await Mediator.Publish(new OrderModifiedEvent(Mapper.Map<WorkerOrderViewDto>(order)), cancellationToken);
        }
    }
}
