using System.Collections.Generic;

namespace Cocorico.Domain.Entities
{
    public sealed class Sandwich
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<SandwichIngredient> SandwichIngredients { get; } = new List<SandwichIngredient>();
        public ICollection<SandwichOrder> SandwichOrders { get; set; } = new List<SandwichOrder>();
        public int Price { get; set; }
    }
}
