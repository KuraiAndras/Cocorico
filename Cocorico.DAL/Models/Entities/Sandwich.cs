using System.Collections.Generic;
using System.Linq;
using Cocorico.Shared.Dtos.Sandwich;

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

        public SandwichDto ToSandwichDto() =>
            this.MapTo(_ => new SandwichDto { Ingredients = SandwichIngredients.Select(il => il.Ingredient.ToIngredientDto()).ToList() });
    }

    public static class SandwichExtensions
    {
        public static Sandwich ToSandwich(this SandwichAddDto sandwichAddDto) =>
            sandwichAddDto.MapTo(_ => new Sandwich { SandwichIngredients = new List<SandwichIngredient>(), });

        public static Sandwich ToSandwich(this SandwichDto sandwichDto) =>
            sandwichDto.MapTo<SandwichDto, Sandwich>();

        public static IEnumerable<Ingredient> Ingredients(this Sandwich sandwich) =>
            sandwich.SandwichIngredients.Select(i => i.Ingredient);
    }
}
