using System;

namespace Cocorico.Shared.Dtos.Ingredient
{
    public class IngredientDto : IEquatable<IngredientDto>
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        public bool Equals(IngredientDto other) =>
            !(other is null) && (ReferenceEquals(this, other) || (Id == other.Id && string.Equals(Name, other.Name)));

        public override bool Equals(object obj) =>
            !(obj is null) && (ReferenceEquals(this, obj) || (obj.GetType() == this.GetType() && Equals((IngredientDto)obj)));

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Name.GetHashCode());
            }
        }
    }
}
