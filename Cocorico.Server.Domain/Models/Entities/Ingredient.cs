using Cocorico.Shared.Dtos.Ingredient;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Cocorico.Server.Domain.Models.Entities
{
    public class Ingredient : IDbEntity<int>, IEquatable<Ingredient>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        public ICollection<SandwichIngredient> SandwichLinks { get; set; } = null!;

        [Required]
        public bool IsDeleted { get; set; }

        #region GeneratedEqualatyMembers

        public bool Equals(Ingredient other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Name, other.Name) && IsDeleted == other.IsDeleted;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Ingredient)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ IsDeleted.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Ingredient left, Ingredient right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Ingredient left, Ingredient right)
        {
            return !Equals(left, right);
        }

        #endregion

        public IngredientDto ToIngredientDto() =>
            this.MapTo<Ingredient, IngredientDto>();
    }

    public static class IngredientExtension
    {
        public static Ingredient ToIngredient(this IngredientDto ingredientDto) =>
            ingredientDto.MapTo<IngredientDto, Ingredient>();
    }
}
