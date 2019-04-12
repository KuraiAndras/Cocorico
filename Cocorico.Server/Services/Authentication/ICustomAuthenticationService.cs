using Cocorico.Shared.Dtos.Authentication;
using System.Threading.Tasks;

namespace Cocorico.Server.Services.Authentication
{
    public interface ICustomAuthenticationService
    {
        Task RegisterAsync(RegisterDetails model);
        Task<LoginResult> LoginAsync(LoginDetails model);
        Task LogoutAsync();
    }
}
