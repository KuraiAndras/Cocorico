using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Order
{
    public class CustomerViewOrderDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("sandwiches")]
        public ICollection<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();

        [JsonPropertyName("price")]
        public int Price { get; set; }

        [JsonPropertyName("state")]
        public OrderState State { get; set; }
    }
}
