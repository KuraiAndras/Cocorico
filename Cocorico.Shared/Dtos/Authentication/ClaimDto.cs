namespace Cocorico.Shared.Dtos.Authentication
{
    public sealed class ClaimDto
    {
        public string ClaimType { get; set; } = string.Empty;
        public string ClaimValue { get; set; } = string.Empty;
    }
}