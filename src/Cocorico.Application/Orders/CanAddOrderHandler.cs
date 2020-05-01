using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Orders;
using Cocorico.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Orders
{
    public sealed class CanAddOrderHandler : RequestHandlerBase<CanAddOrder, bool>
    {
        public CanAddOrderHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<bool> Handle(CanAddOrder request, CancellationToken cancellationToken)
        {
            var lastOpeningEnd = await Context.Openings
                .AsNoTracking()
                .OrderByDescending(o => o.End)
                .FirstOrDefaultAsync(cancellationToken);

            if (lastOpeningEnd is null) throw new UnexpectedException("No Opening in Database");

            return request.RequestTime < lastOpeningEnd.End && request.RequestTime > lastOpeningEnd.Start;
        }
    }
}
