using Cocorico.Application.Common.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches.Commands.DeleteSandwich
{
    public class DeleteSandwichCommandHandler : AsyncRequestHandler<DeleteSandwichCommand>
    {
        private readonly ICocoricoDbContext _context;

        public DeleteSandwichCommandHandler(ICocoricoDbContext context) =>
            _context = context;

        protected override async Task Handle(DeleteSandwichCommand request, CancellationToken cancellationToken)
        {
            var sandwichToDelete = await _context.Sandwiches.SingleOrDefaultAsync(s => s.Id.Equals(request.Id), cancellationToken);

            _context.Sandwiches.Remove(sandwichToDelete);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
