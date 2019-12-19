using System.Threading.Tasks;
using Cocorico.Client.Application.Services.Authentication;
using Microsoft.AspNetCore.Components;

namespace Cocorico.Client.Application.ViewModels.NavMenu
{
    public class NavMenuViewModel : INavMenuViewModel
    {
        private readonly ICocoricoAuthenticationStateProvider _authStateProvider;
        private readonly NavigationManager _navigationManager;

        private bool _collapseNavMenu = true;

        public NavMenuViewModel(ICocoricoAuthenticationStateProvider authStateProvider, NavigationManager navigationManager)
        {
            _authStateProvider = authStateProvider;
            _navigationManager = navigationManager;
        }

        public string NavMenuCssClass => _collapseNavMenu ? "collapse" : string.Empty;

        public void ToggleNavMenu() => _collapseNavMenu = !_collapseNavMenu;

        public async Task LogoutAsync()
        {
            await _authStateProvider.LogoutAsync();

            _navigationManager.NavigateTo("/");
        }
    }
}