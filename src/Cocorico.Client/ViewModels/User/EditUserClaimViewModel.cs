﻿using Cocorico.Client.HttpClient;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Dtos.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.User
{
    public class EditUserClaimViewModel : IEditUserClaimViewModel
    {
        private readonly IAuthenticationClient _authenticationClient;

        private readonly IUserClient _userClient;

        public EditUserClaimViewModel(
            IAuthenticationClient authenticationClient,
            IUserClient userClient)
        {
            _authenticationClient = authenticationClient;
            _userClient = userClient;
            UserForAdminPage = new UserForAdminPage { Claims = new List<string>() };
        }

        public UserForAdminPage UserForAdminPage { get; private set; }

        public async Task LoadUserAsync(string userId)
        {
            try
            {
                UserForAdminPage = await _userClient.GetUserForAdminPageAsync(userId);
            }
            catch (SwaggerException)
            {
            }
        }

        public async Task AddClaimToUserAsync(string claimValue, string userId)
        {
            try
            {
                await _authenticationClient.AddClaimToUserAsync(new UserClaimRequest
                {
                    UserId = userId,
                    CocoricoClaim = new CocoricoClaim { ClaimValue = claimValue },
                });

                await LoadUserAsync(userId);
            }
            catch (SwaggerException)
            {
            }
        }

        public async Task RemoveClaimFromUserAsync(string claimValue, string userId)
        {
            try
            {
                await _authenticationClient.RemoveClaimFromUserAsync(new UserClaimRequest
                {
                    UserId = userId,
                    CocoricoClaim = new CocoricoClaim { ClaimValue = claimValue },
                });

                await LoadUserAsync(userId);
            }
            catch (SwaggerException)
            {
            }
        }
    }
}