using Cocorico.Shared.Dtos.Authentication;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.Services.Authentication
{
    public interface ICocoricoClientAuthenticationService
    {
        Task RegisterAsync(RegisterDetails model);
        Task LoginAsync(LoginDetails model);
        Task LogoutAsync();
        Task AddClaimToUserAsync(UserClaimRequest userClaimRequest);
        Task RemoveClaimFromUserAsync(UserClaimRequest userClaimRequest);

        IEnumerable<string> Claims { get; }

        event Action UserLoggedIn;
        event Action UserLoggedOut;
    }
}
