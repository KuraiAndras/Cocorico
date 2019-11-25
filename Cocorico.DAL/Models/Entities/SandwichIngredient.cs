﻿namespace Cocorico.DAL.Models.Entities
{
    public class SandwichIngredient : IDbEntity
    {
        public int SandwichId { get; set; }
        public Sandwich Sandwich { get; set; } = null!;

        public int IngredientId { get; set; }
        public Ingredient Ingredient { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
