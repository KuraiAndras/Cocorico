using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Ingredients;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Ingredients
{
    public sealed class AddIngredientHandler : HandlerBase<AddIngredient>
    {
        public AddIngredientHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(AddIngredient request, CancellationToken cancellationToken)
        {
            var ingredientToAdd = Mapper.Map<Ingredient>(request);

            await Context.Ingredients.AddAsync(ingredientToAdd, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
