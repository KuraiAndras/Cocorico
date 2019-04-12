using Blazored.LocalStorage;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Cocorico.Client.Helpers
{
    //TODO: Return results
    public class AppState
    {
        //TODO: Background worker for continuously checking authentication
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public event Action UserLoggedIn;
        public event Action UserLoggedOut;

        public bool IsLoggedIn { get; private set; }

        private readonly List<string> _userClaims = new List<string>();

        public IEnumerable<string> UserClaims => _userClaims.AsEnumerable();

        public AppState(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;

            CheckLoginStatusAsync();
        }

        public async Task Login(LoginDetails loginDetails)
        {
            var response = await _httpClient.PostAsync(Urls.Server.Login, new StringContent(Json.Serialize(loginDetails), Encoding.UTF8, Verbs.ApplicationJson));

            if (response.IsSuccessStatusCode)
            {
                await SaveClaims(response);
                await UpdateLocalClaims();

                IsLoggedIn = true;
                UserLoggedIn?.Invoke();
            }
        }

        public async Task Logout()
        {
            await _httpClient.PostAsync(Urls.Server.Logout, new StringContent("", Encoding.UTF8, Verbs.ApplicationJson));

            await _localStorage.RemoveItem(Verbs.Claims);

            IsLoggedIn = false;
            UserLoggedOut?.Invoke();
        }

        private async void CheckLoginStatusAsync()
        {
            //TODO: check server instead of local storage
            var claims = await _localStorage.GetItem<IEnumerable<string>>(Verbs.Claims);

            if (claims is null)
            {
                UserLoggedOut?.Invoke();
                return;
            }

            var claimsList = claims.ToList();

            IsLoggedIn = claimsList.Contains(Claims.User);

            if (IsLoggedIn)
            {
                await UpdateLocalClaims();
                UserLoggedIn?.Invoke();
            }
            else
            {
                UserLoggedOut?.Invoke();
            }
        }

        private async Task SaveClaims(HttpResponseMessage responseMessage)
        {
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var loginResult = Json.Deserialize<LoginResult>(responseContent);

            await _localStorage.SetItem(Verbs.Claims, loginResult.Claims);
        }

        private async Task UpdateLocalClaims()
        {
            var claims = await _localStorage.GetItem<IEnumerable<string>>(Verbs.Claims);

            _userClaims.Clear();
            _userClaims.AddRange(claims);
        }
    }
}
