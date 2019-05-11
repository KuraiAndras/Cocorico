using System.Threading.Tasks;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Components;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Cocorico.Client.Blazor.ComponentModels.Authentication
{
    public class LoginModel : ComponentBase
    {
        [Inject] private ICocoricoClientAuthenticationService CocoricoClientAuthenticationService { get; set; }
        [Inject] private IUriHelper UriHelper { get; set; }

        protected LoginDetails LoginDetails { get; } = new LoginDetails();
        protected bool ShowLoginFailed { get; private set; }

        protected async Task Login()
        {
            var result = await CocoricoClientAuthenticationService.LoginAsync(LoginDetails);

            switch (result)
            {
                case Success<LoginResult> _:
                    UriHelper.NavigateTo(Urls.Client.Home);
                    break;
                default:
                    ShowLoginFailed = true;
                    break;
            }
        }
    }
}
