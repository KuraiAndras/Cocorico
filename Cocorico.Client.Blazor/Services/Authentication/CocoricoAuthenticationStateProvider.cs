﻿using Blazor.Extensions.Storage;
using Cocorico.Client.Domain.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.Services.Authentication
{
    public class CocoricoAuthenticationStateProvider : AuthenticationStateProvider, ICocoricoAuthenticationStateProvider
    {
        private readonly LocalStorage _localStorage;
        private readonly IAuthenticationClient _authenticationClient;

        public CocoricoAuthenticationStateProvider(
            LocalStorage localStorage,
            IAuthenticationClient authenticationClient)
        {
            _localStorage = localStorage;
            _authenticationClient = authenticationClient;
        }

        public async Task LoginAsync(LoginDetails loginDetails)
        {
            var result = await _authenticationClient.LoginAsync(loginDetails);

            await _localStorage.SetItem(Verbs.Claims, result.Claims);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public async Task LogoutAsync()
        {
            var response = await _authenticationClient.LogoutAsync();

            if (!response.IsSuccessfulStatusCode()) throw new UnexpectedException();

            await _localStorage.RemoveItem(Verbs.Claims);

            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var storedClaims = await _localStorage.GetItem<IEnumerable<string>>(Verbs.Claims);
            if (storedClaims is null) return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var claims = storedClaims.ToList();
            if (claims.Count == 0) return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));

            var identity = claims.Count != 0
                ? new ClaimsIdentity(claims.Select(c => new Claim(ClaimTypes.Role, c)), "server authentication")
                : new ClaimsIdentity();

            return new AuthenticationState(new ClaimsPrincipal(identity));
        }
    }
}