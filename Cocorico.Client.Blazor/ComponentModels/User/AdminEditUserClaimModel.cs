using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Dtos.User;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.User
{
    public class AdminEditUserClaimModel : ComponentBase
    {
        [Parameter] public string UserId { get; set; }

        [Inject] public ICocoricoClientAuthenticationService AuthenticationService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUserClient UserHttpClient { get; set; }
        protected UserForAdminPage UserForAdminPage { get; private set; }

        protected override async Task OnInitializedAsync() => await LoadUserAsync();

        private async Task LoadUserAsync()
        {
            try
            {
                var result = await UserHttpClient.GetUserForAdminPageAsync(UserId);

                UserForAdminPage = result;
            }
            catch (Exception)
            {
                //TODO: Handle fail
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
