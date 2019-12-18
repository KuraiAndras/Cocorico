using Cocorico.Shared.Dtos.Authentication;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.Authentication
{
    public interface IServerCocoricoAuthenticationService
    {
        Task RegisterAsync(RegisterDetails model);
        Task<ClaimsDto> LoginAsync(LoginDetails model);
        Task LogoutAsync();
        Task AddClaimToUserAsync(UserClaimRequest userClaimRequest);
        Task RemoveClaimFromUserAsync(UserClaimRequest userClaimRequest);
    }
}
