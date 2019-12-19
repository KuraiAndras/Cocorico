namespace Cocorico.Application.Ingredients.Commands.DeleteIngredient
{
    // TODO: Explicit dto class
    public sealed class DeleteIngredientCommand : CommandDtoBase<int>
    {
        public DeleteIngredientCommand(int dto) : base(dto)
        {
        }
    }
}
