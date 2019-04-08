using System;

// ReSharper disable NonReadonlyMemberInGetHashCode
namespace Cocorico.Shared.Dtos.Sandwich
{
    public class SandwichResultDto : IEquatable<SandwichResultDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Equals(SandwichResultDto other) =>
            !(other is null)
            && (ReferenceEquals(this, other)
                || (Id == other.Id
                    && string.Equals(Name, other.Name)));

        public override bool Equals(object obj) =>
            !(obj is null)
            && (ReferenceEquals(this, obj)
                || (obj is SandwichResultDto sandwichResultDto && Equals(sandwichResultDto)));

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public static bool operator ==(SandwichResultDto left, SandwichResultDto right) => Equals(left, right);

        public static bool operator !=(SandwichResultDto left, SandwichResultDto right) => !Equals(left, right);
    }
}
