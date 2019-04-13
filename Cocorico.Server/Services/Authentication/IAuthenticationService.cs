using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Services.Helpers;
using System.Threading.Tasks;

namespace Cocorico.Server.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<IServiceResult> RegisterAsync(RegisterDetails model);
        Task<IServiceResult<LoginResult>> LoginAsync(LoginDetails model);
        Task<IServiceResult> LogoutAsync();
    }
}
