﻿using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Services;
using System.Net.Http;

// ReSharper disable UnusedAutoPropertyAccessor.Local
namespace Cocorico.Client.ComponentModels.Authentication
{
    public class RegisterModel : ComponentBase
    {
        [Inject] private IUriHelper UriHelper { get; set; }
        [Inject] private HttpClient HttpClient { get; set; }

        protected RegisterDetails RegisterDetails { get; } = new RegisterDetails();

        protected async void Register()
        {
            await HttpClient.PostJsonAsync(Urls.Server.Register, RegisterDetails);

            UriHelper.NavigateTo("/");
        }
    }
}
