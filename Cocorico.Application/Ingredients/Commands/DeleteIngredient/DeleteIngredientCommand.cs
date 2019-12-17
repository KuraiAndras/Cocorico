using MediatR;

namespace Cocorico.Application.Ingredients.Commands.DeleteIngredient
{
    public sealed class DeleteIngredientCommand : IRequest
    {
        public DeleteIngredientCommand(int id) => Id = id;

        public int Id { get; }
    }
}
