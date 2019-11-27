using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Ingredient
{
    public class IngredientAddDto
    {
        [JsonPropertyName("name")]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
    }
}
