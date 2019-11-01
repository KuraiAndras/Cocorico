using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Cocorico.Client.Blazor.ComponentModels.Authentication
{
    public class LoginModel : ComponentBase
    {
        [Inject] private ICocoricoClientAuthenticationService CocoricoClientAuthenticationService { get; set; }
        [Inject] private NavigationManager UriHelper { get; set; }

        protected LoginDetails LoginDetails { get; } = new LoginDetails();
        protected bool ShowLoginFailed { get; private set; }

        protected async Task Login()
        {
            try
            {
                await CocoricoClientAuthenticationService.LoginAsync(LoginDetails);

                UriHelper.NavigateTo(Urls.Client.Home);
            }
            catch (Exception)
            {
                ShowLoginFailed = true;
            }
        }
    }
}
