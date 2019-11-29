using System;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Opening
{
    public class AddOpeningDto
    {
        [JsonPropertyName("start")]
        public DateTime? Start { get; set; }

        [JsonPropertyName("end")]
        public DateTime? End { get; set; }
    }
}