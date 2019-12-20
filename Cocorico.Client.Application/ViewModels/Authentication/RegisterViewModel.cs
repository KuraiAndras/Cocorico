using Cocorico.HttpClient;
using Cocorico.HttpClient.Extensions;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Exceptions;
using System;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.Authentication
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
            }
            catch (Exception)
            {
                ShowRegisterFailed = true;
            }
        }
    }
}
