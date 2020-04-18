using AutoMapper;
using Cocorico.Application.Orders.Notifications;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders
{
    public sealed class UpdateOrderHandler : HandlerBase<UpdateOrder>
    {
        public UpdateOrderHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(UpdateOrder request, CancellationToken cancellationToken)
        {
            var order = await Context.Orders
                            .SingleOrDefaultAsync(o => o.Id == request.OrderId, cancellationToken)
                        ?? throw new EntityNotFoundException($"Order not found with id:{request.OrderId}");

            order.State = request.State;

            Context.Orders.Update(order);

            await Context.SaveChangesAsync(cancellationToken);

            await Mediator.Publish(new OrderModifiedEvent(Mapper.Map<WorkerOrderViewDto>(order)), cancellationToken);
        }
    }
}
