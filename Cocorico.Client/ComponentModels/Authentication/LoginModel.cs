using Cocorico.Shared.Dtos.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System.Threading.Tasks;
using Cocorico.Client.Services.Authentication;
using Cocorico.Shared.Services.Helpers;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Cocorico.Client.ComponentModels.Authentication
{
    public class LoginModel : ComponentBase
    {
        [Inject] private IUserAuthenticationService UserAuthenticationService { get; set; }
        [Inject] private IUriHelper UriHelper { get; set; }

        protected LoginDetails LoginDetails { get; } = new LoginDetails();
        protected bool ShowLoginFailed { get; private set; }

        protected async Task Login()
        {
            var result = await UserAuthenticationService.LoginAsync(LoginDetails);

            switch (result)
            {
                case Success _:
                    UriHelper.NavigateTo("/");
                    break;
                default:
                    ShowLoginFailed = true;
                    break;
            }
        }
    }
}
