using Cocorico.Client.Helpers;
using Cocorico.Shared.Dtos.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System.Threading.Tasks;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Cocorico.Client.ComponentModels.Authentication
{
    public class LoginModel : ComponentBase
    {
        [Inject] private AppState AppState { get; set; }
        [Inject] private IUriHelper UriHelper { get; set; }

        protected LoginDetails LoginDetails { get; } = new LoginDetails();
        protected bool ShowLoginFailed { get; private set; }

        protected async Task Login()
        {
            await AppState.Login(LoginDetails);

            if (AppState.IsLoggedIn)
                UriHelper.NavigateTo("/");
            else
            {
                ShowLoginFailed = true;
            }
        }
    }
}
