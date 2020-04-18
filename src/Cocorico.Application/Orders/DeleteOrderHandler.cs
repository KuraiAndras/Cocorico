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
    public sealed class DeleteOrderHandler : HandlerBase<DeleteOrder>
    {
        public DeleteOrderHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteOrder request, CancellationToken cancellationToken)
        {
            var orderToDelete = await Context.Orders.SingleOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (orderToDelete is null) throw new EntityNotFoundException();

            Context.Orders.Remove(orderToDelete);

            await Context.SaveChangesAsync(cancellationToken);

            await Mediator.Publish(new OrderDeletedEvent(orderToDelete.Id), cancellationToken);
        }
    }
}
