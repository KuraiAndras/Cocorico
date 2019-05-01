using Blazored.LocalStorage;
using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.Services.Authentication
{
    // ReSharper disable once UnusedMember.Global
    public class CocoricoClientAuthenticationService : ICocoricoClientAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        private readonly SemaphoreLocker _localStorageServiceLock = new SemaphoreLocker();

        private readonly List<string> _userClaims = new List<string>();

        public IEnumerable<string> Claims => _userClaims;

        private event Func<Task> ServiceStarted;

        public event Action UserLoggedIn;
        public event Action UserLoggedOut;

        public CocoricoClientAuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;

            ServiceStarted = UpdateAuthStateAsync;
            ServiceStarted.Invoke();
        }

        public async Task<IServiceResult> RegisterAsync(RegisterDetails registerDetails)
        {
            var response = await _httpClient.PostJsonWithResultAsync(Urls.Server.Register, registerDetails);

            return response.GetServiceResult();
        }

        public async Task<IServiceResult<LoginResult>> LoginAsync(LoginDetails loginDetails)
        {
            var exception = new InvalidCredentialsException();
            var result = await _httpClient.RetrieveDataFromServerAsync<LoginDetails, LoginResult>(HttpVerbs.Post, Urls.Server.Login, loginDetails, exception);

            switch (result)
            {
                case Success<LoginResult> success:
                    await _localStorageServiceLock.LockAsync(async () => await _localStorageService.SetItemAsync(Verbs.Claims, success.Data.Claims));

                    await UpdateAuthStateAsync();

                    return new Success<LoginResult>(success.Data);
                default: return new Fail<LoginResult>(exception);
            }
        }

        public async Task<IServiceResult> LogoutAsync()
        {
            var response = await _httpClient.PostJsonWithResultAsync(Urls.Server.Logout, "");

            if (!response.IsSuccessStatusCode) return new Fail(new UnexpectedException());

            await _localStorageServiceLock.LockAsync(async () => await _localStorageService.RemoveItemAsync(Verbs.Claims));

            await UpdateAuthStateAsync();

            return new Success();
        }

        public async Task<IServiceResult> AddClaimToUserAsync(UserClaimRequest userClaimRequest)
        {
            var response = await _httpClient.RetrieveMessageFromServerAsync(HttpVerbs.Post, Urls.Server.AddClaimToUser, userClaimRequest, new InvalidCommandException());

            switch (response)
            {
                case Success success: return success;
                default: return new Fail(new InvalidCommandException());
            }
        }

        public async Task<IServiceResult> RemoveClaimFromUserAsync(UserClaimRequest userClaimRequest)
        {
            var response = await _httpClient.RetrieveMessageFromServerAsync(HttpVerbs.Post, Urls.Server.RemoveClaimFromUser, userClaimRequest, new InvalidCommandException());

            switch (response)
            {
                case Success success: return success;
                default: return new Fail(new InvalidCommandException());
            }
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
