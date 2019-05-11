using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.Services.Authentication
{
    public interface ICocoricoClientAuthenticationService
    {
        Task<IServiceResult> RegisterAsync(RegisterDetails model);
        Task<IServiceResult<LoginResult>> LoginAsync(LoginDetails model);
        Task<IServiceResult> LogoutAsync();
        Task<IServiceResult> AddClaimToUserAsync(UserClaimRequest userClaimRequest);
        Task<IServiceResult> RemoveClaimFromUserAsync(UserClaimRequest userClaimRequest);

        IEnumerable<string> Claims { get; }

        event Action UserLoggedIn;
        event Action UserLoggedOut;
    }
}
