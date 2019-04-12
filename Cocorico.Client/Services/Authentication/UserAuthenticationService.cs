using Blazored.LocalStorage;
using Cocorico.Client.Extensions;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cocorico.Client.Services.Authentication
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        private readonly SemaphoreLocker _localStorageServiceLock = new SemaphoreLocker();
        private readonly SemaphoreLocker _httpClientLock = new SemaphoreLocker();

        private bool _isLoggedIn;

        public bool IsLoggedIn
        {
            get => _isLoggedIn;
            private set
            {
                _isLoggedIn = value;
                if (_isLoggedIn)
                {
                    UserLoggedIn?.Invoke();
                }
                else
                {
                    UserLoggedOut?.Invoke();
                }
            }
        }

        private readonly List<string> _userClaims = new List<string>();

        public IReadOnlyCollection<string> Claims => _userClaims.ToList();

        public event Action UserLoggedIn;
        public event Action UserLoggedOut;

        public UserAuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService)
        {
            _httpClient = httpClient;
            _localStorageService = localStorageService;

            UpdateAuthStateAsync().RunSynchronously();
        }

        public async Task<IServiceResult> RegisterAsync(RegisterDetails registerDetails)
        {
            IServiceResult result = new Fail(new UnexpectedException());
            await _httpClientLock.LockAsync(async () =>
            {
                var response = await _httpClient.PostJsonWithResultAsync(Urls.Server.Register, registerDetails);

                result = response.GetServiceResult();
            });

            return result;
        }

        public async Task<IServiceResult> LoginAsync(LoginDetails loginDetails)
        {
            IServiceResult result = new Fail(new UnexpectedException());
            await _httpClientLock.LockAsync(async () =>
            {
                var response = await _httpClient.PostJsonWithResultAsync(Urls.Server.Login, loginDetails);

                if (response.IsSuccessStatusCode) await SaveClaimsAsync(response);

                result = response.GetServiceResult(new EntityNotFoundException());

                await UpdateAuthStateAsync();
            });

            return result;
        }

        public async Task<IServiceResult> LogoutAsync()
        {
            IServiceResult result = new Fail(new UnexpectedException());
            await _httpClientLock.LockAsync(async () =>
            {
                var response = await _httpClient.PostJsonWithResultAsync(Urls.Server.Logout, "");

                result = response.GetServiceResult();
            });

            await _localStorageServiceLock.LockAsync(async () => await _localStorageService.RemoveItem(Verbs.Claims));

            await UpdateAuthStateAsync();

            return result;
        }

        private async Task SaveClaimsAsync(HttpResponseMessage responseMessage)
        {
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var loginResult = Json.Deserialize<LoginResult>(responseContent);

            await _localStorageServiceLock.LockAsync(async () =>
                await _localStorageService.SetItem(Verbs.Claims, loginResult.Claims));
        }

        private async Task UpdateAuthStateAsync()
        {
            await _localStorageServiceLock.LockAsync(async () =>
            {
                var claims = (await _localStorageService.GetItem<IEnumerable<string>>(Verbs.Claims)).ToList();

                _userClaims.Clear();

                if (claims.Any())
                {
                    _userClaims.AddRange(claims);

                    IsLoggedIn = true;
                }
                else
                {
                    IsLoggedIn = false;
                }
            });
        }
    }
}
