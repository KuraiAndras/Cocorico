using Cocorico.Shared.Dtos.Ingredients;

namespace Cocorico.Application.Ingredients.Commands.AddIngredient
{
    public sealed class AddIngredientCommand : CommandDtoBase<IngredientAddDto>
    {
        public AddIngredientCommand(IngredientAddDto dto)
            : base(dto)
        {
        }
    }
}
