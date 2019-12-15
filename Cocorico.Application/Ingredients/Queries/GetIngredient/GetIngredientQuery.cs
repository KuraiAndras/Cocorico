using Cocorico.Shared.Dtos.Ingredient;
using MediatR;

namespace Cocorico.Application.Ingredients.Queries.GetIngredient
{
    public sealed class GetIngredientQuery : IRequest<IngredientDto>
    {
        public GetIngredientQuery(int id) => Id = id;

        public int Id { get; }
    }
}
