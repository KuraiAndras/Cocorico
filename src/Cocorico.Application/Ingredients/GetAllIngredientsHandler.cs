using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Shared.Api.Ingredients;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Ingredients
{
    public sealed class GetAllIngredientsHandler : RequestHandlerBase<GetAllIngredients, ICollection<IngredientDto>>
    {
        public GetAllIngredientsHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        public override async Task<ICollection<IngredientDto>> Handle(GetAllIngredients request, CancellationToken cancellationToken)
        {
            var ingredients = await Context.Ingredients.ToListAsync(cancellationToken);

            return Mapper.Map<ICollection<IngredientDto>>(ingredients);
        }
    }
}
