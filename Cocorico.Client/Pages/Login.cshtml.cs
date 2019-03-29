using Cocorico.Client.Helpers;
using Cocorico.Shared.Dtos.Jwt;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System.Threading.Tasks;

namespace Cocorico.Client.Pages
{
    public class LoginModel : ComponentBase
    {
        [Inject] private AppState _appState { get; set; }
        [Inject] private IUriHelper _uriHelper { get; set; }

        protected LoginDetails LoginDetails { get; set; } = new LoginDetails();
        protected bool ShowLoginFailed { get; set; }

        protected async Task Login()
        {
            await _appState.Login(LoginDetails);

            if (_appState.IsLoggedIn)
            {
                _uriHelper.NavigateTo("/");
            }
            else
            {
                ShowLoginFailed = true;
            }
        }
    }
}
