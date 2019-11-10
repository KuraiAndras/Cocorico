using Cocorico.Client.Domain.Exceptions;
using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.Authentication
{
    public class RegisterViewModel : IRegisterViewModel
    {
        private readonly NavigationManager _uriHelper;
        private readonly IAuthenticationClient _authenticationClient;

        public RegisterViewModel(NavigationManager uriHelper, IAuthenticationClient authenticationClient)
        {
            _uriHelper = uriHelper;
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

                _uriHelper.NavigateTo(Urls.Client.Login);
            }
            catch (Exception)
            {
                ShowRegisterFailed = true;
            }
        }
    }
}
