using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;

// ReSharper disable NonReadonlyMemberInGetHashCode
namespace Cocorico.Shared.Dtos.Sandwich
{
    public class SandwichAddDto
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; }
    }
}
