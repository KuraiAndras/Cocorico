using Cocorico.Shared.Entities;

namespace Cocorico.Shared.Api.Ingredients
{
    public class IngredientModificationDto
    {
        public int IngredientId { get; set; }
        public int SandwichId { get; set; }
        public Modifier Modification { get; set; }
    }
}
