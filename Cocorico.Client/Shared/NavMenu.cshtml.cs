using Cocorico.Client.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Layouts;
using System.Threading.Tasks;

namespace Cocorico.Client.Shared
{
    public class NavMenuModel : LayoutComponentBase
    {
        [Inject] protected AppState AppState { get; set; }

        protected async Task Logout() => await AppState.Logout();
    }
}
