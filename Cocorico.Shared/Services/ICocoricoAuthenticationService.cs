using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Services.Helpers;
using System.Threading.Tasks;

namespace Cocorico.Shared.Services
{
    public interface ICocoricoAuthenticationService
    {
        Task<IServiceResult> RegisterAsync(RegisterDetails model);
        Task<IServiceResult<LoginResult>> LoginAsync(LoginDetails model);
        Task<IServiceResult> LogoutAsync();
        Task<IServiceResult> AddClaimToUserAsync(UserClaimRequest userClaimRequest);
        Task<IServiceResult> RemoveClaimFromUserAsync(UserClaimRequest userClaimRequest);
    }
}
