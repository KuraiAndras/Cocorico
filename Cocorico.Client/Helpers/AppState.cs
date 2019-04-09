using Blazored.LocalStorage;
using Cocorico.Shared.Dtos.Jwt;
using Cocorico.Shared.Helpers;
using Microsoft.JSInterop;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
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
        public bool IsAdmin { get; private set; }

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
                await SaveTokenAndRoles(response);
                await SetAuthorizationHeader();
                await CheckRoles();


                IsLoggedIn = true;
                UserLoggedIn?.Invoke();
            }
        }

        public async Task Logout()
        {
            var response = await _httpClient.PostAsync(Urls.Server.Logout, new StringContent("", Encoding.UTF8, Verbs.ApplicationJson));

            if (!response.IsSuccessStatusCode) return;

            await _localStorage.RemoveItem(Verbs.AuthToken);
            await _localStorage.RemoveItem(Verbs.Roles);
            //TODO: Reset _httpClient header

            IsLoggedIn = false;
            UserLoggedOut?.Invoke();
        }

        private async void CheckLoginStatusAsync()
        {
            var token = await _localStorage.GetItem<string>(Verbs.AuthToken);

            IsLoggedIn = !string.IsNullOrEmpty(token);

            if (IsLoggedIn)
            {
                await SetAuthorizationHeader();
                await CheckRoles();
                UserLoggedIn?.Invoke();
            }
            else
            {
                UserLoggedOut?.Invoke();
            }
        }

        private async Task SaveTokenAndRoles(HttpResponseMessage responseMessage)
        {
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var jwt = Json.Deserialize<LoginResult>(responseContent);

            await _localStorage.SetItem(Verbs.AuthToken, jwt.Jwt.Token);

            var roles = jwt.Roles.Aggregate("", (current, role) => current + role + " ");

            await _localStorage.SetItem(Verbs.Roles, roles);
        }

        private async Task CheckRoles()
        {
            var roles = await _localStorage.GetItem<string>(Verbs.Roles);

            //TODO: remove CocoricoUser
            if (roles.Contains("Admin") || roles.Contains("CocoricoUser"))
            {
                IsAdmin = true;
            }
        }

        private async Task SetAuthorizationHeader()
        {
            if (!_httpClient.DefaultRequestHeaders.Contains(Verbs.Authorization))
            {
                var token = await _localStorage.GetItem<string>(Verbs.AuthToken);
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(Verbs.Bearer, token);
            }
        }
    }
}
