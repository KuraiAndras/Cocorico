using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;

// ReSharper disable NonReadonlyMemberInGetHashCode
namespace Cocorico.Shared.Dtos.Sandwich
{
    public class SandwichDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int Price { get; set; }

        public List<IngredientDto> Ingredients { get; set; }
    }
}
