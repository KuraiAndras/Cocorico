using Cocorico.Shared.Helpers;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Ingredient
{
    public class IngredientModificationDto
    {
        [JsonPropertyName("ingredientId")]
        public int IngredientId { get; set; }

        [JsonPropertyName("sandwichId")]
        public int SandwichId { get; set; }

        [JsonPropertyName("modification")]
        public Modifier Modification { get; set; }
    }
}
