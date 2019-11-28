using System.Collections.Generic;
using System.Linq;

namespace Cocorico.DAL.Models.Entities
{
    public class Sandwich : IDbEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<SandwichIngredient> SandwichIngredients { get; set; } = null!;
        public ICollection<SandwichOrder> SandwichOrders { get; set; } = null!;
        public int Price { get; set; }
        public bool IsDeleted { get; set; }
    }

    public static class SandwichExtensions
    {
        public static ICollection<Ingredient> Ingredients(this Sandwich sandwich) =>
            sandwich.SandwichIngredients.Select(i => i.Ingredient).ToList();
    }
}
