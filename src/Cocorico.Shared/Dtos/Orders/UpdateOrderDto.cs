using Cocorico.Shared.Entities;

namespace Cocorico.Shared.Dtos.Orders
{
    public class UpdateOrderDto
    {
        public int OrderId { get; set; }
        public OrderState State { get; set; }
    }
}
