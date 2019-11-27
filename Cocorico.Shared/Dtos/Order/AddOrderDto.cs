using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Order
{
    public class AddOrderDto
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;

        [JsonPropertyName("sandwiches")]
        public IList<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();

        [JsonPropertyName("customerId")]
        public string CustomerId { get; set; } = string.Empty;
    }
}
