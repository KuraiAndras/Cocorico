using Cocorico.Client.Services.Authentication;
using Cocorico.Shared.Api.Users;
using System;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Authentication
{
    public class LoginViewModel : ILoginViewModel
    {
        private readonly ICocoricoAuthenticationStateProvider _authStateProvider;

        public LoginViewModel(ICocoricoAuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
            UserLoginDetails = new LoginUser();
        }

        public bool ShowLoginFailed { get; private set; }
        public LoginUser UserLoginDetails { get; }

        public async Task LoginUserAsync()
        {
            try
            {
                await _authStateProvider.LoginAsync(UserLoginDetails);
            }
            catch (Exception)
            {
                ShowLoginFailed = true;
            }
        }
    }
}
