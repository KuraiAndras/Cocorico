using Cocorico.Shared.Dtos.Jwt;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System.Net.Http;

namespace Cocorico.Client.ComponentModels.Authentication
{
    public class RegisterModel : ComponentBase
    {
        [Inject] private IUriHelper UriHelper { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }

        protected RegisterDetails RegisterDetails { get; set; } = new RegisterDetails();

        protected async void Register()
        {
            await HttpClient.PostJsonAsync(Urls.Server.Register, RegisterDetails);

            UriHelper.NavigateTo("/");
        }
    }
}
