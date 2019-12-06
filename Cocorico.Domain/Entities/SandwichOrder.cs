using System.Collections.Generic;

namespace Cocorico.Domain.Entities
{
    public sealed class SandwichOrder
    {
        public int Id { get; set; }

        public int SandwichId { get; set; }
        public Sandwich Sandwich { get; set; } = null!;

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public ICollection<IngredientModification> IngredientModifications { get; } = new List<IngredientModification>();
    }
}
