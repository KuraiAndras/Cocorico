using Cocorico.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using Microsoft.AspNetCore.Components.Services;
using System.Threading.Tasks;

namespace Cocorico.Client.ComponentModels.Navigation
{
    public class NavMenuModel : LayoutComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Global
        [Inject] protected AppState AppState { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }

        protected async Task Logout()
        {
            await AppState.Logout();

            UriHelper.NavigateTo("/");
        }

        protected override void OnInit()
        {
            AppState.UserLoggedIn += UserLoggedIn;
            AppState.UserLoggedOut += UserLoggedOut;
        }

        private void UserLoggedOut() => StateHasChanged();
        private void UserLoggedIn() => StateHasChanged();
    }
}
