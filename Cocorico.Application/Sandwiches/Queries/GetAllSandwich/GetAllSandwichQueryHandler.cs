using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Shared.Dtos.Sandwich;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Sandwiches.Queries.GetAllSandwich
{
    public sealed class GetAllSandwichQueryHandler : QueryHandlerBase<GetAllSandwichQuery, ICollection<SandwichDto>>
    {
        public GetAllSandwichQueryHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<ICollection<SandwichDto>> Handle(GetAllSandwichQuery request, CancellationToken cancellationToken)
        {
            var sandwiches = await Context
                .Sandwiches
                .Include(s => s.SandwichIngredients)
                .ThenInclude(il => il.Ingredient)
                .ToListAsync(cancellationToken);

            return sandwiches.Select(s => Mapper.Map<SandwichDto>(s)).ToList();
        }
    }
}
