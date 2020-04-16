using Cocorico.Shared.Dtos.Ingredients;
using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Sandwiches
{
    public class SandwichDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public ICollection<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
    }
}
