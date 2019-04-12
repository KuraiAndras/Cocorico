using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using Microsoft.AspNetCore.Components.Services;
using System.Threading.Tasks;
using Cocorico.Client.Services.Authentication;

namespace Cocorico.Client.ComponentModels.Navigation
{
    public class NavMenuModel : LayoutComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] protected IUserAuthenticationService UserAuthenticationService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        protected async Task Logout()
        {
            await UserAuthenticationService.LogoutAsync();

            UriHelper.NavigateTo("/");
        }

        protected override void OnInit()
        {
            UserAuthenticationService.UserLoggedIn += UserLoggedIn;
            UserAuthenticationService.UserLoggedOut += UserLoggedOut;
        }

        private void UserLoggedOut() => StateHasChanged();
        private void UserLoggedIn() => StateHasChanged();
    }
}
