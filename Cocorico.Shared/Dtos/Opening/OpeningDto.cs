using System;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Opening
{
    public class OpeningDto
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("start")]
        public DateTime? Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime? End { get; set; }

        [JsonPropertyName("numberOfOrders")]
        public int NumberOfOrders { get; set; }
    }
}
