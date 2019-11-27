using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Authentication
{
    public class UserClaimRequest
    {
        [JsonPropertyName("userId")]
        public string UserId { get; set; } = string.Empty;

        [JsonPropertyName("cocoricoClaim")]
        public CocoricoClaim CocoricoClaim { get; set; } = new CocoricoClaim();
    }
}
