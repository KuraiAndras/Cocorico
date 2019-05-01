using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.Navigation
{
    public class NavMenuModel : LayoutComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private ICocoricoClientAuthenticationService CocoricoClientAuthenticationService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        protected bool IsCustomer => CocoricoClientAuthenticationService.Claims.Contains(Claims.Customer);

        protected bool IsLoggedIn { get; private set; }

        protected bool IsAdmin => CocoricoClientAuthenticationService.Claims.Contains(Claims.Admin);

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

        private void UserLoggedOut()
        {
            IsLoggedIn = false;
            StateHasChanged();
        }

        private void UserLoggedIn()
        {
            IsLoggedIn = true;
            StateHasChanged();
        }
    }
}
