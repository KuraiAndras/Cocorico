using System;

// ReSharper disable NonReadonlyMemberInGetHashCode
namespace Cocorico.Shared.Dtos.Sandwich
{
    public class NewSandwichDto : IEquatable<NewSandwichDto>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }

        public bool Equals(NewSandwichDto other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(Name, other.Name) && Price == other.Price;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((NewSandwichDto) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (Name != null ? Name.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Price;
                return hashCode;
            }
        }
    }
}
