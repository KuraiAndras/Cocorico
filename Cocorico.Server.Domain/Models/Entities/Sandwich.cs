using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Linq;

namespace Cocorico.Server.Domain.Models.Entities
{
    public class Sandwich : IDbEntity<int>
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ICollection<SandwichIngredient> IngredientLinks { get; set; } = null!;
        public ICollection<UserSandwichOrder> OrderLinks { get; set; } = null!;
        public int Price { get; set; }
        public bool IsDeleted { get; set; }

        public SandwichDto ToSandwichDto() =>
            this.MapTo(_ => new SandwichDto { Ingredients = IngredientLinks.Select(il => il.Ingredient.ToIngredientDto()).ToList() });
    }

    public static class SandwichExtensions
    {
        public static Sandwich ToSandwich(this SandwichAddDto sandwichAddDto) =>
            sandwichAddDto.MapTo(_ => new Sandwich { IngredientLinks = new List<SandwichIngredient>(), });

        public static Sandwich ToSandwich(this SandwichDto sandwichDto) =>
            sandwichDto.MapTo<SandwichDto, Sandwich>();

        public static IEnumerable<Ingredient> Ingredients(this Sandwich sandwich) =>
            sandwich.IngredientLinks.Select(i => i.Ingredient);
    }
}
