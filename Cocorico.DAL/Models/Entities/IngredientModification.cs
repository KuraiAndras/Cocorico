using Cocorico.Shared.Helpers;

namespace Cocorico.DAL.Models.Entities
{
    public class IngredientModification : IDbEntity<int>
    {
        public int Id { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        public int SandwichOrderId { get; set; }
        public SandwichOrder SandwichOrder { get; set; } = null!;

        public Modifier Modification { get; set; }

        public bool IsDeleted { get; set; }
    }
}
