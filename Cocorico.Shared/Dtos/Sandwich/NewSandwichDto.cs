using System;

// ReSharper disable NonReadonlyMemberInGetHashCode
namespace Cocorico.Shared.Dtos.Sandwich
{
    public class NewSandwichDto : IEquatable<NewSandwichDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Equals(NewSandwichDto other) =>
            !(other is null)
            && (ReferenceEquals(this, other)
                || (Id == other.Id
                    && string.Equals(Name, other.Name)));

        public override bool Equals(object obj) =>
            !(obj is null)
            && (ReferenceEquals(this, obj)
                || (obj is NewSandwichDto newSandwichDto && Equals(newSandwichDto)));

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Name?.GetHashCode() ?? 0);
            }
        }

        public static bool operator ==(NewSandwichDto left, NewSandwichDto right) => Equals(left, right);

        public static bool operator !=(NewSandwichDto left, NewSandwichDto right) => !Equals(left, right);
    }
}
