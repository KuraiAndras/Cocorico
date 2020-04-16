using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Shared.Dtos.Authentication;
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
            UserRegisterDetails = new RegisterDetails();
        }

        public RegisterDetails UserRegisterDetails { get; }
        public bool ShowRegisterFailed { get; private set; }

        public async Task RegisterUserAsync()
        {
            try
            {
                var response = await _authenticationClient.RegisterAsync(UserRegisterDetails);

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
