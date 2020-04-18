using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Sandwiches;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches
{
    public sealed class GetAllSandwichesHandler : RequestHandlerBase<GetAllSandwiches, ICollection<SandwichDto>>
    {
        public GetAllSandwichesHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<ICollection<SandwichDto>> Handle(GetAllSandwiches request, CancellationToken cancellationToken)
        {
            var sandwiches = await Context
                .Sandwiches
                .Include(s => s.SandwichIngredients)
                .ThenInclude(si => si.Ingredient)
                .ToListAsync(cancellationToken);

            return Mapper.Map<ICollection<SandwichDto>>(sandwiches);
        }
    }
}
