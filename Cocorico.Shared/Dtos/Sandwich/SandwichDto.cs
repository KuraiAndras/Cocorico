using Cocorico.Shared.Dtos.Ingredient;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Sandwich
{
    public class SandwichDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("ingredients")]
        public ICollection<IngredientDto> Ingredients { get; set; } = new List<IngredientDto>();
    }
}
