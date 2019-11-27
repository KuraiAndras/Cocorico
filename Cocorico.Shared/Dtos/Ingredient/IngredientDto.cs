using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Ingredient
{
    public class IngredientDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
