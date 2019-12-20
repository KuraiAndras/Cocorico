using Cocorico.Client.Application.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.Authentication
{
    public class LoginViewModel : ILoginViewModel
    {
        private readonly ICocoricoAuthenticationStateProvider _authStateProvider;

        public bool ShowLoginFailed { get; private set; }
        public LoginDetails UserLoginDetails { get; }

        public LoginViewModel(ICocoricoAuthenticationStateProvider authStateProvider)
        {
            _authStateProvider = authStateProvider;
            UserLoginDetails = new LoginDetails();
        }

        public async Task LoginUserAsync()
        {
            try
            {
                await _authStateProvider.LoginAsync(UserLoginDetails);
            }
            catch
            {
                ShowLoginFailed = true;
            }
        }
    }
}
