using AutoMapper;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Ingredients.Commands.AddIngredient
{
    public sealed class AddIngredientCommandHandler : CommandHandlerBase<AddIngredientCommand>
    {
        public AddIngredientCommandHandler(
            IMediator mediator,
            IMapper mapper,
            CocoricoDbContext context)
            : base(mediator, mapper, context)
        {
        }

        protected override async Task Handle(AddIngredientCommand request, CancellationToken cancellationToken)
        {
            var ingredientToAdd = Mapper.Map<Ingredient>(request.Dto);

            await Context.Ingredients.AddAsync(ingredientToAdd, cancellationToken);

            await Context.SaveChangesAsync(cancellationToken);
        }
    }
}
