using Cocorico.Domain.Entities;

namespace Cocorico.Shared.Dtos.Ingredient
{
    public class IngredientModificationDto
    {
        public int IngredientId { get; set; }
        public int SandwichId { get; set; }
        public Modifier Modification { get; set; }
    }
}
