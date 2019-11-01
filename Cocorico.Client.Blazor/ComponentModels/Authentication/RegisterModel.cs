using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Cocorico.Client.Blazor.ComponentModels.Authentication
{
    public class RegisterModel : ComponentBase
    {
        [Inject] private NavigationManager UriHelper { get; set; }
        [Inject] private ICocoricoClientAuthenticationService CocoricoClientAuthenticationService { get; set; }

        protected RegisterDetails RegisterDetails { get; } = new RegisterDetails();

        protected bool ShowRegisterFailed { get; private set; }

        protected async void Register()
        {
            try
            {
                await CocoricoClientAuthenticationService.RegisterAsync(RegisterDetails);

                UriHelper.NavigateTo(Urls.Client.Login);
            }
            catch (Exception)
            {
                ShowRegisterFailed = true;
                StateHasChanged();
            }
        }
    }
}
