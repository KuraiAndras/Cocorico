using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

// ReSharper disable NonReadonlyMemberInGetHashCode
namespace Cocorico.Server.Api.Models.Entities.Sandwich
{
    public class Sandwich : IDbEntity<int>, IEquatable<Sandwich>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsDeleted { get; set; }

        public bool Equals(Sandwich other) =>
            !(other is null)
            && (ReferenceEquals(this, other)
                || (Id == other.Id
                    && string.Equals(Name, other.Name)));

        public override bool Equals(object obj) =>
            !(obj is null)
            && (ReferenceEquals(this, obj)
                || (obj is Sandwich sandwich && Equals(sandwich)));

        public override int GetHashCode()
        {
            unchecked
            {
                return (Id * 397) ^ (Name != null ? Name.GetHashCode() : 0);
            }
        }

        public static bool operator ==(Sandwich left, Sandwich right) => Equals(left, right);

        public static bool operator !=(Sandwich left, Sandwich right) => !Equals(left, right);
    }
}
