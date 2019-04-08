using System.Threading.Tasks;
using Cocorico.Shared.Dtos.Jwt;

namespace Cocorico.Server.Services.Authentication
{
    public interface ICustomAuthenticationService
    {
        Task RegisterAsync(RegisterDetails model);
        Task<LoginResult> LoginAsync(LoginDetails model);
        Task LogoutAsync();
    }
}
