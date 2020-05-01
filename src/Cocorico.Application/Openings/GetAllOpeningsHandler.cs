using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Openings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Openings
{
    public sealed class GetAllOpeningsHandler : RequestHandlerBase<GetAllOpenings, ICollection<OpeningDto>>
    {
        public GetAllOpeningsHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<ICollection<OpeningDto>> Handle(GetAllOpenings request, CancellationToken cancellationToken)
        {
            var openingsInDb = await Context.Openings
                .AsNoTracking()
                .Include(o => o.Orders)
                .AsNoTracking()
                .ToListAsync(cancellationToken);

            return Mapper.Map<ICollection<OpeningDto>>(openingsInDb);
        }
    }
}
