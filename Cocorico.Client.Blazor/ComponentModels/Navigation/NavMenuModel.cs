using System.Threading.Tasks;
using Cocorico.Client.Domain.Services.Authentication;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using Microsoft.AspNetCore.Components.Services;

namespace Cocorico.Client.Blazor.ComponentModels.Navigation
{
    public class NavMenuModel : LayoutComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] protected ICocoricoClientAuthenticationService CocoricoClientAuthenticationService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        protected async Task Logout()
        {
            await CocoricoClientAuthenticationService.LogoutAsync();

            UriHelper.NavigateTo("/");
        }

        protected override void OnInit()
        {
            CocoricoClientAuthenticationService.UserLoggedIn += UserLoggedIn;
            CocoricoClientAuthenticationService.UserLoggedOut += UserLoggedOut;
        }

        private void UserLoggedOut() => StateHasChanged();
        private void UserLoggedIn() => StateHasChanged();
    }
}
