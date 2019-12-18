using Blazored.LocalStorage;
using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Domain.Exceptions;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.Services.Authentication
{
    public class CocoricoAuthenticationStateProvider : AuthenticationStateProvider, ICocoricoAuthenticationStateProvider
    {
        private readonly ILocalStorageService _localStorage;
        private readonly IAuthenticationClient _authenticationClient;

        public CocoricoAuthenticationStateProvider(
            ILocalStorageService localStorage,
            IAuthenticationClient authenticationClient)
        {
            _localStorage = localStorage;
            _authenticationClient = authenticationClient;
        }

        public async Task LoginAsync(LoginDetails loginDetails)
        {
            var result = await _authenticationClient.LoginAsync(loginDetails);

            await _localStorage.SetItemAsync(Verbs.Claims, result.Claims);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task LogoutAsync()
        {
            var response = await _authenticationClient.LogoutAsync();

            if (!response.IsSuccessfulStatusCode()) throw new UnexpectedException();

            await _localStorage.RemoveItemAsync(Verbs.Claims);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var storedClaims = await _localStorage.GetItemAsync<ClaimsDto>(Verbs.Claims);

            var authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            if (storedClaims is null || storedClaims.Claims.Count == 0) return authenticationState;

            var identity = new ClaimsIdentity(storedClaims.Claims);

            authenticationState.User.AddIdentity(identity);

            return authenticationState;
        }
    }
}
