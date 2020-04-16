using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Ingredients.Commands.UpdateIngredient
{
    public sealed class UpdateIngredientCommandHandler : CommandHandlerBase<UpdateIngredientCommand>
    {
        public UpdateIngredientCommandHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(UpdateIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientToUpdate = Mapper.Map<Ingredient>(request.Dto);

            Context.Ingredients.Update(ingredientToUpdate);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
