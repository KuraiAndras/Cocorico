using Cocorico.Shared.Dtos.Authentication;
using System.Threading.Tasks;

namespace Cocorico.Shared.Services
{
    public interface ICocoricoAuthenticationService
    {
        Task RegisterAsync(RegisterDetails model);
        Task<LoginResult> LoginAsync(LoginDetails model);
        Task LogoutAsync();
        Task AddClaimToUserAsync(UserClaimRequest userClaimRequest);
        Task RemoveClaimFromUserAsync(UserClaimRequest userClaimRequest);
    }
}
