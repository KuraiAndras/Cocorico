﻿using System.Text.Json.Serialization;

namespace Cocorico.Shared.Dtos.Authentication
{
    public class RegisterDetails
    {
        [JsonPropertyName("email")]
        public string Email { get; set; } = string.Empty;

        [JsonPropertyName("password")]
        public string Password { get; set; } = string.Empty;

        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;
    }
}
