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
        public string CocoricoUserId { get; set; } = null!;
        public CocoricoUser CocoricoUser { get; set; } = null!;
        public ICollection<SandwichOrder> SandwichOrders { get; set; } = null!;
        public int Price { get; set; }
        public OrderState State { get; set; }
        public bool IsDeleted { get; set; }

        public OrderWorkerViewDto ToOrderWorkerViewDto() =>
            this.MapTo(o => new OrderWorkerViewDto
            {
                UserName = o.CocoricoUser.Name,
                Sandwiches = o.Sandwiches().Select(s => s.MapTo(sa => new SandwichDto
                {
                    Ingredients = sa.SandwichIngredients.Select(i => i.Ingredient.ToIngredientDto()).ToList()
                }))
            });
    }

    public static class OrderExtension
    {
        public static IEnumerable<Sandwich> Sandwiches(this Order order) =>
            order.SandwichOrders.Select(sl => sl.Sandwich);
    }
}
