using Cocorico.Shared.Dtos.Authentication;
using System.Threading.Tasks;
using Cocorico.Server.Helpers;

namespace Cocorico.Server.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<IServiceResult> RegisterAsync(RegisterDetails model);
        Task<IServiceResult<LoginResult>> LoginAsync(LoginDetails model);
        Task<IServiceResult> LogoutAsync();
    }
}
