using System.Text.Json.Serialization;

namespace Cocorico.Shared.Helpers
{
    public class MutableRange
    {
        [JsonPropertyName("start")]
        public int Start { get; set; }

        [JsonPropertyName("end")]
        public int End { get; set; }
    }
}