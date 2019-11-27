using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Authentication
{
    public class CocoricoClaim
    {
        [JsonPropertyName("claimValue")]
        public string ClaimValue { get; set; } = string.Empty;
    }
}
