using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public ICollection<Ingredient> Ingredients { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public bool IsDeleted { get; set; }
    }
}
