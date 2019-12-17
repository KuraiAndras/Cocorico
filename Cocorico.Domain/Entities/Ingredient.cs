using System.Collections.Generic;

namespace Cocorico.Domain.Entities
{
    public sealed class Ingredient
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<SandwichIngredient> SandwichIngredients { get; } = new List<SandwichIngredient>();
        public ICollection<IngredientModification> IngredientModifications { get; } = new List<IngredientModification>();
    }
}
