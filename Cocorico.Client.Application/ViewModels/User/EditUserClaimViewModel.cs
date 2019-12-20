using System.Collections.Generic;
using System.Threading.Tasks;
using Cocorico.HttpClient;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Dtos.User;

namespace Cocorico.Client.Application.ViewModels.User
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
                //TODO: Handle fail
            }
        }

        public async Task AddClaimToUserAsync(string claimValue, string userId)
        {
            try
            {
                await _authenticationClient.AddClaimToUserAsync(new UserClaimRequest
                {
                    UserId = userId,
                    CocoricoClaim = new CocoricoClaim { ClaimValue = claimValue }
                });

                await LoadUserAsync(userId);
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        public async Task RemoveClaimFromUserAsync(string claimValue, string userId)
        {
            try
            {
                await _authenticationClient.RemoveClaimFromUserAsync(new UserClaimRequest
                {
                    UserId = userId,
                    CocoricoClaim = new CocoricoClaim { ClaimValue = claimValue }
                });

                await LoadUserAsync(userId);
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }
    }
}