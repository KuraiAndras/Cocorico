using Cocorico.Shared.Dtos.Ingredients;

namespace Cocorico.Application.Ingredients.Queries.GetIngredient
{
    public sealed class GetIngredientQuery : QueryDtoBase<int, IngredientDto>
    {
        public GetIngredientQuery(int dto)
            : base(dto)
        {
        }
    }
}
