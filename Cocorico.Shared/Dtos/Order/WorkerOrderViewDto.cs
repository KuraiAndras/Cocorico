using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Order
{
    public class WorkerOrderViewDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userName")]
        public string UserName { get; set; } = string.Empty;

        [JsonPropertyName("sandwiches")]
        public ICollection<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();

        [Range(0, 31)]
        public int RotatingId { get; set; }

        [JsonPropertyName("state")]
        public OrderState State { get; set; }
    }
}
