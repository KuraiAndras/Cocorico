using Cocorico.Client.Domain.Services.User;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.User
{
    public class UsersModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IClientUserService UserService { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }
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

        protected void GoToEdit(string userId) => UriHelper.NavigateTo(Urls.Client.AdminEditUserClaim + $"/{userId}");
    }
}
