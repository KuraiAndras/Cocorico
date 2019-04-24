using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Cocorico.Shared.Helpers;

// ReSharper disable NonReadonlyMemberInGetHashCode
namespace Cocorico.Server.Domain.Models.Entities
{
    public class Order : IDbEntity<int>, IEquatable<Order>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string CustomerId { get; set; }

        [Required]
        [ForeignKey(nameof(CustomerId))]
        public CocoricoUser Customer { get; set; }

        public ICollection<Sandwich> Sandwiches { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public OrderState State { get; set; }

        [Required]
        public bool IsDeleted { get; set; }


        #region GeneratedEqualatyMembers

        public bool Equals(Order other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id && string.Equals(CustomerId, other.CustomerId) && Equals(Customer, other.Customer) && Equals(Sandwiches, other.Sandwiches) && Price == other.Price && State == other.State && IsDeleted == other.IsDeleted;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Order) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = Id;
                hashCode = (hashCode * 397) ^ (CustomerId != null ? CustomerId.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Customer != null ? Customer.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Sandwiches != null ? Sandwiches.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ Price;
                hashCode = (hashCode * 397) ^ (int) State;
                hashCode = (hashCode * 397) ^ IsDeleted.GetHashCode();
                return hashCode;
            }
        }

        public static bool operator ==(Order left, Order right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Order left, Order right)
        {
            return !Equals(left, right);
        }

        #endregion
    }
}
