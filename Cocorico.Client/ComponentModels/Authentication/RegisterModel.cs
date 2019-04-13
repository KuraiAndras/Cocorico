using Cocorico.Client.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Cocorico.Client.ComponentModels.Authentication
{
    public class RegisterModel : ComponentBase
    {
        [Inject] private IUriHelper UriHelper { get; set; }
        [Inject] private IUserAuthenticationService UserAuthenticationService { get; set; }

        protected RegisterDetails RegisterDetails { get; } = new RegisterDetails();

        protected bool ShowRegisterFailed { get; private set; }

        protected async void Register()
        {
            var result = await UserAuthenticationService.RegisterAsync(RegisterDetails);

            switch (result)
            {
                case Success _:
                    UriHelper.NavigateTo(Urls.Client.Login);
                    break;
                default:
                    ShowRegisterFailed = true;
                    StateHasChanged();
                    break;
            }
        }
    }
}
