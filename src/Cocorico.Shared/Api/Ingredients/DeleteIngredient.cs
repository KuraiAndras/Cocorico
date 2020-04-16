using MediatR;

namespace Cocorico.Shared.Api.Ingredients
{
    public sealed class DeleteIngredient : IRequest
    {
        public int Id { get; set; }
    }
}
