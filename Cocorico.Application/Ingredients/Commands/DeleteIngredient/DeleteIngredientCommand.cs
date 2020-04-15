namespace Cocorico.Application.Ingredients.Commands.DeleteIngredient
{
    public sealed class DeleteIngredientCommand : CommandDtoBase<int>
    {
        public DeleteIngredientCommand(int dto) : base(dto)
        {
        }
    }
}
