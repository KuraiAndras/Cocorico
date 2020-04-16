using Cocorico.Shared.Entities;

namespace Cocorico.Persistence.Entities
{
    public sealed class IngredientModification
    {
        public int Id { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        public int SandwichOrderId { get; set; }
        public SandwichOrder SandwichOrder { get; set; } = null!;

        public Modifier Modification { get; set; }
    }
}
