﻿using Blazored.LocalStorage;
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
    // ReSharper disable once UnusedMember.Global
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorageService;

        private readonly SemaphoreLocker _localStorageServiceLock = new SemaphoreLocker();

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

        public IEnumerable<string> Claims => _userClaims;

        private event Func<Task> ServiceStarted;

        public event Action UserLoggedIn;
        public event Action UserLoggedOut;

        public UserAuthenticationService(HttpClient httpClient, ILocalStorageService localStorageService)
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

        public async Task<IServiceResult> LoginAsync(LoginDetails loginDetails)
        {
            var response = await _httpClient.PostJsonWithResultAsync(Urls.Server.Login, loginDetails);

            if (response.IsSuccessStatusCode) await SaveClaimsAsync(response);

            await UpdateAuthStateAsync();

            return response.GetServiceResult(new EntityNotFoundException());
        }

        public async Task<IServiceResult> LogoutAsync()
        {
            var response = await _httpClient.PostJsonWithResultAsync(Urls.Server.Logout, "");

            await _localStorageServiceLock.LockAsync(async () => await _localStorageService.RemoveItem(Verbs.Claims));

            await UpdateAuthStateAsync();

            return response.GetServiceResult();
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
                var claims = await _localStorageService.GetItem<IEnumerable<string>>(Verbs.Claims);

                if (claims is null)
                {
                    IsLoggedIn = false;
                    return;
                }

                var claimList = claims.ToList();

                if (claimList.Any())
                {
                    _userClaims.Clear();
                    _userClaims.AddRange(claimList);

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
