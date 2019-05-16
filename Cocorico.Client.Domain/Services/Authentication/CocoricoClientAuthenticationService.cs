using Blazored.LocalStorage;
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
    // ReSharper disable once UnusedMember.Global
    public class CocoricoClientAuthenticationService : ICocoricoClientAuthenticationService
    {
        private readonly ILocalStorageService _localStorageService;
        private readonly IAuthenticationClient _authenticationClient;

        private readonly SemaphoreLocker _localStorageServiceLock = new SemaphoreLocker();

        private readonly List<string> _userClaims = new List<string>();

        public IEnumerable<string> Claims => _userClaims;

        private event Func<Task> ServiceStarted;

        public event Action UserLoggedIn;
        public event Action UserLoggedOut;

        public CocoricoClientAuthenticationService(
            ILocalStorageService localStorageService,
            IAuthenticationClient authenticationClient)
        {
            _localStorageService = localStorageService;
            _authenticationClient = authenticationClient;

            ServiceStarted = UpdateAuthStateAsync;
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

            await _localStorageServiceLock.LockAsync(async () => await _localStorageService.SetItemAsync(Verbs.Claims, result.Claims));

            await UpdateAuthStateAsync();
        }

        public async Task LogoutAsync()
        {
            var response = await _authenticationClient.LogoutAsync();

            if (!response.IsSuccessfulStatusCode()) throw new UnexpectedException();

            await _localStorageServiceLock.LockAsync(async () => await _localStorageService.RemoveItemAsync(Verbs.Claims));

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
            await _localStorageServiceLock.LockAsync(async () =>
            {
                var claims = await _localStorageService.GetItemAsync<IEnumerable<string>>(Verbs.Claims);

                if (claims is null)
                {
                    UserLoggedOut?.Invoke();
                    return;
                }

                var claimList = claims.ToList();

                if (claimList.Any())
                {
                    _userClaims.Clear();
                    _userClaims.AddRange(claimList);

                    UserLoggedIn?.Invoke();
                }
                else
                {
                    UserLoggedOut?.Invoke();
                }
            });
        }
    }
}
