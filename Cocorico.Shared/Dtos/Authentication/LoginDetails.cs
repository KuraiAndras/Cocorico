using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Authentication
{
    public class LoginDetails
    {
        [JsonPropertyName("email")]
        [EmailAddress]
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        [StringLength(40, MinimumLength = 5)]
        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; } = string.Empty;
    }
}
