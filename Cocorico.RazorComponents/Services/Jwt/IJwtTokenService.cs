namespace Cocorico.RazorComponents.Services.Jwt
{
    public interface IJwtTokenService
    {
        string BuildToken(string email);
    }
}
