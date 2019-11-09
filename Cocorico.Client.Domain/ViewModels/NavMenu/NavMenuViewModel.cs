using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.NavMenu
{
    public class NavMenuViewModel : INavMenuViewModel
    {
        private readonly ICocoricoClientAuthenticationService _cocoricoClientAuthenticationService;
        private readonly NavigationManager _navigationManager;

        private bool _collapseNavMenu = true;

        public NavMenuViewModel(ICocoricoClientAuthenticationService cocoricoClientAuthenticationService, NavigationManager navigationManager)
        {
            _cocoricoClientAuthenticationService = cocoricoClientAuthenticationService;
            _navigationManager = navigationManager;
        }

        public string NavMenuCssClass => _collapseNavMenu ? "collapse" : string.Empty;
        public void ToggleNavMenu() => _collapseNavMenu = !_collapseNavMenu;
        public bool IsLoggedIn { get; set; }
        public bool IsCustomer => _cocoricoClientAuthenticationService.Claims.Contains(Claims.Customer);
        public bool IsAdmin => _cocoricoClientAuthenticationService.Claims.Contains(Claims.Admin);
        public bool IsWorker => _cocoricoClientAuthenticationService.Claims.Contains(Claims.Worker);

        public async Task LogoutAsync()
        {
            await _cocoricoClientAuthenticationService.LogoutAsync();

            _navigationManager.NavigateTo("/");
        }

        public void Initialize()
        {
            _cocoricoClientAuthenticationService.UserLoggedIn += UserLoggedIn;
            _cocoricoClientAuthenticationService.UserLoggedOut += UserLoggedOut;
        }

        private void UserLoggedOut() => IsLoggedIn = false;

        private void UserLoggedIn() => IsLoggedIn = true;
    }
}