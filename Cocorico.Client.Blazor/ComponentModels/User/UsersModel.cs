using Cocorico.Client.Domain.Helpers;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor.ComponentModels.User
{
    public class UsersModel : ComponentBase
    {
        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUserClient UserHttpClient { get; set; }

        // ReSharper disable once UnusedAutoPropertyAccessor.Local
        [Inject] private IUriHelper UriHelper { get; set; }
        protected IReadOnlyList<UserForAdminPage> Users { get; private set; } = new List<UserForAdminPage>();

        protected override async Task OnInitAsync() => await LoadUsersAsync();

        private async Task LoadUsersAsync()
        {
            try
            {
                var result = await UserHttpClient.GetAllForAdminAsync();

                Users = result.ToList();
            }
            catch (Exception)
            {
                //TODO: Handle fail
            }
        }

        protected void GoToEdit(string userId) => UriHelper.NavigateTo(Urls.Client.AdminEditUserClaim + $"/{userId}");
    }
}
