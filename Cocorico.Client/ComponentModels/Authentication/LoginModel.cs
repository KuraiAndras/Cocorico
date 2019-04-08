using Cocorico.Client.Helpers;
using Cocorico.Shared.Dtos.Jwt;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System.Threading.Tasks;

namespace Cocorico.Client.ComponentModels.Authentication
{
    public class LoginModel : ComponentBase
    {
        [Inject] private AppState AppState { get; set; }
        [Inject] private IUriHelper UriHelper { get; set; }

        protected LoginDetails LoginDetails { get; set; } = new LoginDetails();
        protected bool ShowLoginFailed { get; set; }

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
