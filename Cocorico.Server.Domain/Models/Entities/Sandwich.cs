using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

// ReSharper disable NonReadonlyMemberInGetHashCode
namespace Cocorico.Server.Domain.Models.Entities
{
    public class Sandwich : IDbEntity<int>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<SandwichIngredient> IngredientLinks { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public SandwichDto ToSandwichDto() =>
            this.MapTo(s => new SandwichDto
            {
                Ingredients = IngredientLinks.Select(il => il.Ingredient.ToIngredientDto()).ToList()
            });
    }

    public static class SandwichExtensions
    {
        public static Sandwich ToSandwich(this SandwichAddDto sandwichAddDto) =>
            sandwichAddDto.MapTo(s => new Sandwich
            {
                IngredientLinks = new List<SandwichIngredient>(),
            });

        public static Sandwich ToSandwich(this SandwichDto sandwichDto) =>
            sandwichDto.MapTo<SandwichDto, Sandwich>();

        public static IEnumerable<Ingredient> Ingredients(this Sandwich sandwich) => sandwich.IngredientLinks.Select(i => i.Ingredient);
    }
}
