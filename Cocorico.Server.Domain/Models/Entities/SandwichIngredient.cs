namespace Cocorico.Server.Domain.Models.Entities
{
    // ReSharper disable once ClassNeverInstantiated.Global
    public class SandwichIngredient
    {
        public int SandwichId { get; set; }
        public Sandwich Sandwich { get; set; }

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; }
    }
}
