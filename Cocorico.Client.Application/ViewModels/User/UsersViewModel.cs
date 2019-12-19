using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cocorico.HttpClient;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components;

namespace Cocorico.Client.Application.ViewModels.User
{
    public class UsersViewModel : IUsersViewModel
    {
        private readonly IUserClient _userHttpClient;
        private readonly NavigationManager _uriHelper;

        public UsersViewModel(IUserClient userHttpClient, NavigationManager uriHelper)
        {
            _userHttpClient = userHttpClient;
            _uriHelper = uriHelper;
            UsersList = new List<UserForAdminPage>();
        }

        public IReadOnlyList<UserForAdminPage> UsersList { get; private set; }

        public async Task LoadUsersAsync()
        {
            try
            {
                var result = await _userHttpClient.GetAllForAdminAsync();

                UsersList = result.ToList();
            }
            catch (SwaggerException)
            {
                //TODO: Handle fail
            }
        }

        public void GoToEdit(string userId) => _uriHelper.NavigateTo(Urls.Client.AdminEditUserClaim + $"/{userId}");
    }
}