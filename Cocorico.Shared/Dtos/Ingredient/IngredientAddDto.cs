using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Ingredient
{
    public class IngredientAddDto
    {
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
