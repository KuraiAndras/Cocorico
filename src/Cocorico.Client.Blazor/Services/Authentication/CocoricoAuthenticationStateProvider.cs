﻿using Cocorico.Client.Extensions;
using Cocorico.Client.HttpClient;
using Cocorico.Client.Services.Authentication;
using Cocorico.Shared.Api.Users;
using Cocorico.Shared.Exceptions;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.Services.Authentication
{
    public class CocoricoAuthenticationStateProvider : AuthenticationStateProvider, ICocoricoAuthenticationStateProvider
    {
        private readonly IAuthenticationClient _authenticationClient;

        public CocoricoAuthenticationStateProvider(IAuthenticationClient authenticationClient) =>
            _authenticationClient = authenticationClient;

        public async Task LoginAsync(LoginUser loginDetails)
        {
            await _authenticationClient.LoginAsync(loginDetails);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task LogoutAsync()
        {
            var response = await _authenticationClient.LogoutAsync();

            if (!response.IsSuccessfulStatusCode()) throw new UnexpectedException();

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var authenticationState = new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            try
            {
                var claims = await _authenticationClient.GetCurrentUserClaimsAsync();

                authenticationState.User.AddIdentity(new ClaimsIdentity(claims.Claims.Select(c => new Claim(c.ClaimType, c.ClaimValue))));
            }
            catch
            {
                Console.WriteLine("Auth failed");
            }

            return authenticationState;
        }
    }
}
