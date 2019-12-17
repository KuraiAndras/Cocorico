using Cocorico.Domain.Entities;

namespace Cocorico.Shared.Dtos.Order
{
    public class UpdateOrderDto
    {
        public int OrderId { get; set; }
        public OrderState State { get; set; }
    }
}
