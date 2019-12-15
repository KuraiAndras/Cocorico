using Cocorico.Application.Common.Persistence;
using Cocorico.Application.Orders.Notifications.OrderAdded;
using Cocorico.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : AsyncRequestHandler<DeleteOrderCommand>
    {
        private readonly ICocoricoDbContext _context;
        private readonly IMediator _mediator;

        public DeleteOrderCommandHandler(
            ICocoricoDbContext context,
            IMediator mediator)
        {
            _context = context;
            _mediator = mediator;
        }

        protected override async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _context.Orders.SingleOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (orderToDelete is null) throw new EntityNotFoundException();

            _context.Orders.Remove(orderToDelete);

            await _context.SaveChangesAsync(cancellationToken);

            await _mediator.Publish(new OrderDeletedEvent(orderToDelete.Id), cancellationToken);
        }
    }
}