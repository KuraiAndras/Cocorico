using Blazor.Extensions.Storage;
using Cocorico.Client.Domain.Exceptions;
using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.Services.Authentication
{
    public class CocoricoClientAuthenticationService : ICocoricoClientAuthenticationService
    {
        private readonly LocalStorage _localStorage;
        private readonly IAuthenticationClient _authenticationClient;

        private readonly List<string> _userClaims = new List<string>();

        public IEnumerable<string> Claims => _userClaims;

        private event Action ServiceStarted;

        public event Action UserLoggedIn;
        public event Action UserLoggedOut;

        public CocoricoClientAuthenticationService(
            LocalStorage localStorage,
            IAuthenticationClient authenticationClient)
        {
            _localStorage = localStorage;
            _authenticationClient = authenticationClient;

            ServiceStarted = async () => await UpdateAuthStateAsync();
            ServiceStarted.Invoke();
        }

        public async Task RegisterAsync(RegisterDetails registerDetails)
        {
            var response = await _authenticationClient.RegisterAsync(registerDetails);

            if (!response.IsSuccessfulStatusCode()) throw new RegisterFailedException();
        }

        public async Task LoginAsync(LoginDetails loginDetails)
        {
            var result = await _authenticationClient.LoginAsync(loginDetails);

            await _localStorage.SetItem(Verbs.Claims, result.Claims);

            await UpdateAuthStateAsync();
        }

        public async Task LogoutAsync()
        {
            var response = await _authenticationClient.LogoutAsync();

            if (!response.IsSuccessfulStatusCode()) throw new UnexpectedException();

            await _localStorage.RemoveItem(Verbs.Claims);

            await UpdateAuthStateAsync();
        }

        public async Task AddClaimToUserAsync(UserClaimRequest userClaimRequest)
        {
            var response = await _authenticationClient.AddClaimToUserAsync(userClaimRequest);

            if (!response.IsSuccessfulStatusCode()) throw new InvalidCommandException();
        }

        public async Task RemoveClaimFromUserAsync(UserClaimRequest userClaimRequest)
        {
            var response = await _authenticationClient.RemoveClaimFromUserAsync(userClaimRequest);

            if (!response.IsSuccessfulStatusCode()) throw new InvalidCommandException();
        }

        private async Task UpdateAuthStateAsync()
        {
            var claims = await _localStorage.GetItem<IEnumerable<string>>(Verbs.Claims);

            if (claims is null)
            {
                UserLoggedOut?.Invoke();
                return;
            }

            var claimList = claims.ToList();

            _userClaims.Clear();
            if (claimList.Any())
            {
                _userClaims.AddRange(claimList);

                UserLoggedIn?.Invoke();
            }
            else
            {
                UserLoggedOut?.Invoke();
            }
        }
    }
}
