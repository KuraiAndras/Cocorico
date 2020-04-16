using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders.Queries.CanAddOrder
{
    public sealed class CanAddOrderQueryHandler : QueryHandlerBase<CanAddOrderQuery, bool>
    {
        public CanAddOrderQueryHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<bool> Handle(CanAddOrderQuery request, CancellationToken cancellationToken)
        {
            var lastOpeningEnd = await Context.Openings
                .AsNoTracking()
                .OrderByDescending(o => o.End)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastOpeningEnd is null) throw new UnexpectedException("No Opening in Database");

            return request.Dto < lastOpeningEnd.End && request.Dto > lastOpeningEnd.Start;
        }
    }
}
