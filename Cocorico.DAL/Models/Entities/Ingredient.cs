using System.Collections.Generic;

namespace Cocorico.DAL.Models.Entities
{
    public class Ingredient : IDbEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<SandwichIngredient> SandwichIngredients { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
