using Cocorico.Shared.Helpers;
using System;
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
        public DateTime Time { get; set; }
        public int OpeningId { get; set; }
        public Opening Opening { get; set; } = null!;
        public int RotatingId { get; set; }
        public bool IsDeleted { get; set; }
    }

    public static class OrderExtension
    {
        public static ICollection<Sandwich> Sandwiches(this Order order) =>
            order.SandwichOrders.Select(sl => sl.Sandwich).ToList();

        public static IDictionary<Sandwich, ICollection<IngredientModification>> SandwichModifications(this Order order)
        {
            var sandwichDictionary = new Dictionary<Sandwich, ICollection<IngredientModification>>();

            foreach (var sandwichOrder in order.SandwichOrders)
            {
                var modifications = sandwichOrder.IngredientModifications;
                sandwichDictionary.Add(sandwichOrder.Sandwich, modifications);
            }

            return sandwichDictionary;
        }
    }
}
