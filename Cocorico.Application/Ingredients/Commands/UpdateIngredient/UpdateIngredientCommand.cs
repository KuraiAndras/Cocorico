using AutoMapper;
using Cocorico.Application.Common.Persistence;
using Cocorico.Domain.Entities;
using Cocorico.Shared.Dtos.Ingredient;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Cocorico.Application.Ingredients.Commands.UpdateIngredient
{
    public sealed class UpdateIngredientCommand : IRequest
    {
        public UpdateIngredientCommand(IngredientDto dto) => Dto = dto;

        public IngredientDto Dto { get; }
    }

    public sealed class UpdateIngredientCommandHandler : CommandHandlerBase<UpdateIngredientCommand>
    {
        public UpdateIngredientCommandHandler(
            IMediator mediator,
            IMapper mapper,
            ICocoricoDbContext context)
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
