using Cocorico.Domain.Entities;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Order
{
    public class UpdateOrderDto
    {
        [JsonPropertyName("orderId")]
        public int OrderId { get; set; }

        [JsonPropertyName("state")]
        public OrderState State { get; set; }
    }
}
