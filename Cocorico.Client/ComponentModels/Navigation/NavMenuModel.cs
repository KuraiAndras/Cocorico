using Cocorico.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using System.Threading.Tasks;

namespace Cocorico.Client.ComponentModels.Navigation
{
    public class NavMenuModel : LayoutComponentBase
    {
        [Inject] protected AppState AppState { get; set; }

        protected async Task Logout() => await AppState.Logout();

        protected override void OnInit()
        {
            AppState.UserLoggedIn += UserLoggedIn;
            AppState.UserLoggedOut += UserLoggedOut;
        }

        private void UserLoggedOut() => StateHasChanged();
        private void UserLoggedIn() => StateHasChanged();
    }
}
