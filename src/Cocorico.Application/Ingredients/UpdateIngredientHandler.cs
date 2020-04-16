using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Api.Ingredients;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Ingredients
{
    public sealed class UpdateIngredientHandler : RequestHandlerBase<UpdateIngredient>
    {
        public UpdateIngredientHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(UpdateIngredient request, CancellationToken cancellationToken)
        {
            var ingredientToUpdate = Mapper.Map<Ingredient>(request);

            Context.Ingredients.Update(ingredientToUpdate);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
