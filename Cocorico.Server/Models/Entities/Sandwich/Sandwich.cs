using Cocorico.Server.Models.Entities.Contract;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cocorico.Server.Models.Entities.Sandwich
{
    public class Sandwich : IHashAssertable
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Name { get; set; }

        public int GetAssertHash()
        {
            var hashCode = new HashCode();

            hashCode.Add(Id);
            hashCode.Add(Name);

            return hashCode.ToHashCode();
        }
    }
}
