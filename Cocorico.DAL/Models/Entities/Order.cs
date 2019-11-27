using Cocorico.Shared.Dtos.Order;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using System.Collections.Generic;
using System.Linq;

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
    }

    public static class OrderExtension
    {
        public static IEnumerable<Sandwich> Sandwiches(this Order order) =>
            order.SandwichOrders.Select(sl => sl.Sandwich);

        public static WorkerOrderViewDto ToOrderWorkerViewDto(this Order order) =>
            order.MapTo(o => new WorkerOrderViewDto
            {
                UserName = o.CocoricoUser.Name,
                Sandwiches = o.Sandwiches().Select(s => s.MapTo(sa => new SandwichDto
                {
                    Ingredients = sa.SandwichIngredients.Select(i => i.Ingredient.ToIngredientDto()).ToList()
                })).ToList()
            });
    }
}
