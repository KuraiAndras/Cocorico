using MediatR;

namespace Cocorico.Shared.Api.Ingredients
{
    public sealed class GetIngredient : IRequest<IngredientDto>
    {
        public int Id { get; set; }
    }
}
