namespace Cocorico.Shared.Dtos.Jwt
{
    public class JwtModel
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public int ExpiresIn { get; set; }
    }
}
