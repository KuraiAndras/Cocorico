using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Cocorico.Server.Domain.Models.Entities
{
    public class Order : IDbEntity<int>
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
        public bool IsDeleted { get; set; }

        //TODO: State
    }
}
