using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;
using System.Linq;

namespace Cocorico.Server.Domain.Models.Entities
{
    public class Ingredient : IDbEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<SandwichIngredient> SandwichLinks { get; set; } = null!;
        public bool IsDeleted { get; set; }

        public IngredientDto ToIngredientDto() =>
            this.MapTo<Ingredient, IngredientDto>();
    }

    public static class IngredientExtension
    {
        public static Ingredient ToIngredient(this IngredientDto ingredientDto) =>
            ingredientDto.MapTo<IngredientDto, Ingredient>();
    }
}
