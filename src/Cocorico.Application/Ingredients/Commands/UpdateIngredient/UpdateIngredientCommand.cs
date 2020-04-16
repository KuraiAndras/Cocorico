using Cocorico.Shared.Dtos.Ingredients;
using MediatR;

namespace Cocorico.Application.Ingredients.Commands.UpdateIngredient
{
    public sealed class UpdateIngredientCommand : IRequest
    {
        public UpdateIngredientCommand(IngredientDto dto) => Dto = dto;

        public IngredientDto Dto { get; }
    }
}
