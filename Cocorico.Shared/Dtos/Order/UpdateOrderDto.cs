using System.Text.Json.Serialization;
using Cocorico.Shared.Helpers;

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
