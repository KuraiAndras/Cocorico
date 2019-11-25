using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Sandwich
{
    public class SandwichAddDto
    {
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }

        public IEnumerable<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
    }
}
