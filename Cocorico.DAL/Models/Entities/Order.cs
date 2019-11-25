using System.Collections.Generic;
using System.Linq;
using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;

namespace Cocorico.DAL.Models.Entities
{
    public class Order : IDbEntity<int>
    {
        public int Id { get; set; }
        public string CustomerId { get; set; } = null!;
        public CocoricoUser Customer { get; set; } = null!;
        public ICollection<UserSandwichOrder> SandwichLinks { get; set; } = null!;
        public int Price { get; set; }
        public OrderState State { get; set; }
        public bool IsDeleted { get; set; }

        public OrderWorkerViewDto ToOrderWorkerViewDto() =>
            this.MapTo(o => new OrderWorkerViewDto
            {
                UserName = o.Customer.Name,
                Sandwiches = o.Sandwiches().Select(s => s.MapTo(sa => new SandwichDto
                {
                    Ingredients = sa.SandwichIngredients.Select(i => i.Ingredient.ToIngredientDto()).ToList()
                }))
            });
    }

    public static class OrderExtension
    {
        public static IEnumerable<Sandwich> Sandwiches(this Order order) =>
            order.SandwichLinks.Select(sl => sl.Sandwich);
    }
}
