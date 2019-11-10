using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Authentication
{
    public class LoginViewModel : ILoginViewModel
    {
        private readonly NavigationManager _uriHelper;
        private readonly ICocoricoAuthenticationStateProvider _authStateProvider;

        public bool ShowLoginFailed { get; private set; }
        public LoginDetails UserLoginDetails { get; }

        public LoginViewModel(NavigationManager navigationManager, ICocoricoAuthenticationStateProvider authStateProvider)
        {
            _uriHelper = navigationManager;
            _authStateProvider = authStateProvider;
            UserLoginDetails = new LoginDetails();
        }

        public async Task LoginUserAsync()
        {
            try
            {
                await _authStateProvider.LoginAsync(UserLoginDetails);

                _uriHelper.NavigateTo(Urls.Client.Home);
            }
            catch (Exception)
            {
                ShowLoginFailed = true;
            }
        }
    }
}
