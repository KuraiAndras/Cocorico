using Cocorico.Client.Application.Services.Authentication;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.NavMenu
{
    public class NavMenuViewModel : INavMenuViewModel
    {
        private readonly ICocoricoAuthenticationStateProvider _authStateProvider;

        private bool _collapseNavMenu = true;

        public NavMenuViewModel(ICocoricoAuthenticationStateProvider authStateProvider) => _authStateProvider = authStateProvider;

        public string NavMenuCssClass => _collapseNavMenu ? "collapse" : string.Empty;

        public void ToggleNavMenu() => _collapseNavMenu = !_collapseNavMenu;

        public async Task LogoutAsync() => await _authStateProvider.LogoutAsync();
    }
}