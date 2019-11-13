namespace Cocorico.Server.Domain.Models.Entities
{
    public class SandwichIngredient
    {
        public int SandwichId { get; set; }
        public Sandwich Sandwich { get; set; } = null!;

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;
    }
}
