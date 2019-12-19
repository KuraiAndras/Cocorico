using System;
using System.Collections.Generic;
using Cocorico.Shared.Entities;

namespace Cocorico.Domain.Entities
{
    public sealed class Order
    {
        public int Id { get; set; }
        public string CocoricoUserId { get; set; } = null!;
        public CocoricoUser CocoricoUser { get; set; } = null!;
        public ICollection<SandwichOrder> SandwichOrders { get; } = new List<SandwichOrder>();
        public int Price { get; set; }
        public OrderState State { get; set; }
        public DateTime Time { get; set; }
        public int OpeningId { get; set; }
        public Opening Opening { get; set; } = null!;
        public int RotatingId { get; set; }
    }
}
