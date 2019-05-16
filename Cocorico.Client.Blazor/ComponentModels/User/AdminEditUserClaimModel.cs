using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Client.Domain.Services.User;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.User
{
    public class AdminEditUserClaimModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Parameter] private string UserId { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private ICocoricoClientAuthenticationService AuthenticationService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IClientUserService UserService { get; set; }
        protected UserForAdminPage UserForAdminPage { get; private set; }

        protected override async Task OnInitAsync() => await LoadUserAsync();

        private async Task LoadUserAsync()
        {
            var result = await UserService.GetUserForAdminPageAsync(UserId);

            //TODO: Handle fail
            switch (result)
            {
                case Success<UserForAdminPage> success:
                    UserForAdminPage = success.Data;
                    break;
            }
        }

        protected async Task AddClaimToUserAsync(string claimValue)
        {
            try
            {
                await AuthenticationService.AddClaimToUserAsync(new UserClaimRequest
                {
                    UserId = UserId,
                    CocoricoClaim = new CocoricoClaim { ClaimValue = claimValue }
                });

                await LoadUserAsync();
            }
            catch (Exception)
            {
                //TODO: Handle fail
            }
        }

        protected async Task RemoveClaimFromUserAsync(string claimValue)
        {
            try
            {
                await AuthenticationService.RemoveClaimFromUserAsync(new UserClaimRequest
                {
                    UserId = UserId,
                    CocoricoClaim = new CocoricoClaim { ClaimValue = claimValue }
                });

                await LoadUserAsync();
            }
            catch (Exception)
            {
                //TODO: Handle fail
            }
        }
    }
}
