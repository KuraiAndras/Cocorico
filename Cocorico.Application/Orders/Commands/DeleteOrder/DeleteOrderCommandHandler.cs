using System.Threading;
using System.Threading.Tasks;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Cocorico.Application.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommandHandler : AsyncRequestHandler<DeleteOrderCommand>
    {
        private readonly ICocoricoDbContext _context;

        public DeleteOrderCommandHandler(ICocoricoDbContext context) => _context = context;

        protected override async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var orderToDelete = await _context.Orders.SingleOrDefaultAsync(o => o.Id == request.Id, cancellationToken);

            if (orderToDelete is null) throw new EntityNotFoundException();

            _context.Orders.Remove(orderToDelete);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}