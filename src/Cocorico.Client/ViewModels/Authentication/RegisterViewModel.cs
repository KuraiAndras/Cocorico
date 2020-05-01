using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Shared.Api.Users;
using Cocorico.Shared.Exceptions;
using System;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.Authentication
{
    public class RegisterViewModel : IRegisterViewModel
    {
        private readonly IAuthenticationClient _authenticationClient;

        public RegisterViewModel(IAuthenticationClient authenticationClient)
        {
            _authenticationClient = authenticationClient;
            UserRegisterUser = new RegisterUser();
        }

        public RegisterUser UserRegisterUser { get; }
        public bool ShowRegisterFailed { get; private set; }

        public async Task RegisterUserAsync()
        {
            try
            {
                var response = await _authenticationClient.RegisterAsync(UserRegisterUser);

                if (!response.IsSuccessfulStatusCode()) throw new RegisterFailedException();

                response.Dispose();
            }
            catch (Exception)
            {
                ShowRegisterFailed = true;
            }
        }
    }
}
