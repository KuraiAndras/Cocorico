using Cocorico.HttpClient;
using Cocorico.Shared.Dtos.User;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Client.Application.ViewModels.User
{
    public class UsersViewModel : IUsersViewModel
    {
        private readonly IUserClient _userHttpClient;

        public UsersViewModel(IUserClient userHttpClient)
        {
            _userHttpClient = userHttpClient;
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
            }
        }
    }
}
