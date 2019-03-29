using Cocorico.Shared.Dtos.Jwt;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System.Net.Http;

namespace Cocorico.Client.ComponentModels.Authentication
{
    public class RegisterModel : ComponentBase
    {
        [Inject] private IUriHelper _uriHelper { get; set; }
        [Inject] private HttpClient _httpClient { get; set; }

        protected RegisterDetails RegisterDetails { get; set; } = new RegisterDetails();

        protected async void Register()
        {
            await _httpClient.PostJsonAsync(Urls.Server.Register, RegisterDetails);

            _uriHelper.NavigateTo("/");
        }
    }
}
