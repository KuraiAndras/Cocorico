using System.Threading.Tasks;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Services;

namespace Cocorico.Client.Domain.ViewModels.User
{
    public class EditUserClaimViewModel : IEditUserClaimViewModel
    {
        private readonly ICocoricoAuthenticationService _authenticationService;

        private readonly IUserClient _userClient;

        public EditUserClaimViewModel(
            ICocoricoAuthenticationService authenticationService,
            IUserClient userClient)
        {
            _authenticationService = authenticationService;
            _userClient = userClient;
            UserForAdminPage = new UserForAdminPage();
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
                await _authenticationService.AddClaimToUserAsync(new UserClaimRequest
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
                await _authenticationService.RemoveClaimFromUserAsync(new UserClaimRequest
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