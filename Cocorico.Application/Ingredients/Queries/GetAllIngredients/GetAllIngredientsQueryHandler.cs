using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Shared.Dtos.Ingredient;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Ingredients.Queries.GetAllIngredients
{
    public sealed class GetAllIngredientsQueryHandler : QueryHandlerBase<GetAllIngredientsQuery, ICollection<IngredientDto>>
    {
        public GetAllIngredientsQueryHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<ICollection<IngredientDto>> Handle(GetAllIngredientsQuery request, CancellationToken cancellationToken)
        {
            var ingredients = await Context.Ingredients.ToListAsync(cancellationToken);

            return Mapper.Map<ICollection<IngredientDto>>(ingredients);
        }
    }
}