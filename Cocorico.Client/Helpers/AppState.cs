using Blazored.LocalStorage;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.JSInterop;
using System;
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
                await SaveRoles(response);
                await CheckRoles();

                IsLoggedIn = true;
                UserLoggedIn?.Invoke();
            }
        }

        public async Task Logout()
        {
            await _httpClient.PostAsync(Urls.Server.Logout, new StringContent("", Encoding.UTF8, Verbs.ApplicationJson));

            await _localStorage.RemoveItem(Verbs.Roles);

            IsLoggedIn = false;
            UserLoggedOut?.Invoke();
        }

        private async void CheckLoginStatusAsync()
        {
            var roles = await _localStorage.GetItem<string>(Verbs.Roles);

            IsLoggedIn = !string.IsNullOrEmpty(roles);

            if (IsLoggedIn)
            {
                await CheckRoles();
                UserLoggedIn?.Invoke();
            }
            else
            {
                UserLoggedOut?.Invoke();
            }
        }

        private async Task SaveRoles(HttpResponseMessage responseMessage)
        {
            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var loginResult = Json.Deserialize<LoginResult>(responseContent);

            var roles = loginResult.Roles.Aggregate("", (current, role) => current + role + " ");

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
    }
}
