using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Authentication
{
    public class LoginResult
    {
        [JsonPropertyName("claims")]
        public ICollection<string> Claims { get; set; } = new List<string>();
    }
}
