namespace Cocorico.Server.Services.Jwt
{
    public interface IJwtTokenService
    {
        string BuildToken(string email);
    }
}
