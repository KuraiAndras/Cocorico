using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Sandwich
{
    public class SandwichAddDto
    {
        [JsonPropertyName("name")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("ingredients")]
        public ICollection<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
    }
}
