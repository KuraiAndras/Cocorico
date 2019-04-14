using Cocorico.Client.Domain.Services.User;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Shared.Dtos.Authentication;

namespace Cocorico.Client.Blazor.ComponentModels.User
{
    public class UsersModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IClientUserService UserService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private ICocoricoClientAuthenticationService AuthenticationService { get; set; }
        protected IReadOnlyList<UserForAdminPage> Users { get; private set; } = new List<UserForAdminPage>();

        protected override async Task OnInitAsync() => await LoadUsersAsync();

        private async Task LoadUsersAsync()
        {
            var result = await UserService.GetAllUsersForAdminPageAsync();

            //TODO: Handle fail
            switch (result)
            {
                case Success<IEnumerable<UserForAdminPage>> success:
                    Users = success.Data.ToList();
                    break;
            }
        }

        protected async Task AddClaimToUserAsync(string userId, string claim)
        {
            var result = await AuthenticationService.AddClaimToUserAsync(new UserClaimRequest
            {
                CocoricoClaim = new CocoricoClaim { ClaimValue = claim },
                UserId = userId,
            });

            //TODO: Handle fail
            if (result is Success) await LoadUsersAsync();
        }

        protected async Task RemoveClaimFromUserAsync(string userId, string claim)
        {
            var result = await AuthenticationService.RemoveClaimFromUserAsync(new UserClaimRequest
            {
                CocoricoClaim = new CocoricoClaim { ClaimValue = claim },
                UserId = userId,
            });

            //TODO: Handle fail
            if (result is Success) await LoadUsersAsync();
        }
    }
}
