using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders.Queries.CanAddOrder
{
    public sealed class CanAddOrderQueryHandler : IRequestHandler<CanAddOrderQuery, bool>
    {
        private readonly ICocoricoDbContext _context;

        public CanAddOrderQueryHandler(ICocoricoDbContext context) => _context = context;

        public async Task<bool> Handle(CanAddOrderQuery request, CancellationToken cancellationToken)
        {
            var lastOpeningEnd = await _context.Openings
                .AsNoTracking()
                .OrderByDescending(o => o.End)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastOpeningEnd is null) throw new UnexpectedException("No Opening in Database");

            return request.Time < lastOpeningEnd.End && request.Time > lastOpeningEnd.Start;
        }
    }
}
