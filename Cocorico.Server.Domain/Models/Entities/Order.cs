using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

// ReSharper disable NonReadonlyMemberInGetHashCode
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

        public ICollection<SandwichOrder> SandwichLinks { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public OrderState State { get; set; }

        [Required]
        public bool IsDeleted { get; set; }

        public OrderWorkerViewDto ToOrderWorkerViewDto() =>
            this.MapTo(o => new OrderWorkerViewDto
            {
                UserName = o.Customer.Name,
                Sandwiches = o.Sandwiches().Select(s => s.MapTo(sa => new SandwichDto
                {
                    Ingredients = sa.IngredientLinks.Select(i => i.Ingredient.ToIngredientDto()).ToList()
                }))
            });
    }

    public static class OrderExtension
    {
        public static IEnumerable<Sandwich> Sandwiches(this Order order) =>
            order.SandwichLinks.Select(sl => sl.Sandwich);
    }
}
