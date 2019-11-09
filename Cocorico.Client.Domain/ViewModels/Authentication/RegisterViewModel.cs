using Cocorico.Client.Domain.Services.Authentication;
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
        private readonly ICocoricoClientAuthenticationService _cocoricoClientAuthenticationService;

        public RegisterViewModel(NavigationManager uriHelper, ICocoricoClientAuthenticationService cocoricoClientAuthenticationService)
        {
            _uriHelper = uriHelper;
            _cocoricoClientAuthenticationService = cocoricoClientAuthenticationService;
            UserRegisterDetails = new RegisterDetails();
        }

        public RegisterDetails UserRegisterDetails { get; }
        public bool ShowRegisterFailed { get; private set; }

        public async Task RegisterUserAsync()
        {
            try
            {
                await _cocoricoClientAuthenticationService.RegisterAsync(UserRegisterDetails);

                _uriHelper.NavigateTo(Urls.Client.Login);
            }
            catch (Exception)
            {
                ShowRegisterFailed = true;
            }
        }
    }
}
