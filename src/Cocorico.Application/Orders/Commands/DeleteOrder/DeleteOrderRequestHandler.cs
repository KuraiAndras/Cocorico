using AutoMapper;
using Cocorico.Application.Orders.Notifications.OrderAdded;
using Cocorico.Persistence;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderRequestHandler : RequestHandlerBase<DeleteOrderCommand>
    {
        public DeleteOrderRequestHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await Context.Orders.SingleOrDefaultAsync(o => o.Id == request.Dto, cancellationToken);

            if (orderToDelete is null) throw new EntityNotFoundException();

            Context.Orders.Remove(orderToDelete);

            await Context.SaveChangesAsync(cancellationToken);

            await Mediator.Publish(new OrderDeletedEvent(orderToDelete.Id), cancellationToken);
        }
    }
}
