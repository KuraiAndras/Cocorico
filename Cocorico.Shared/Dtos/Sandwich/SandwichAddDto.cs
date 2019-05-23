using System;

// ReSharper disable NonReadonlyMemberInGetHashCode
namespace Cocorico.Shared.Dtos.Sandwich
{
    public class SandwichAddDto : IEquatable<SandwichAddDto>
    {
        public string Name { get; set; }
        public int Price { get; set; }

        public bool Equals(SandwichAddDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return string.Equals(Name, other.Name) && Price == other.Price;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((SandwichAddDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 397;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Price;
                return hashCode;
            }
        }
    }
}
