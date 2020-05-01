using Cocorico.Shared.Api.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.ViewModels.User
{
    public interface IUsersViewModel
    {
        IReadOnlyList<UserForAdminPage> UsersList { get; }
        Task LoadUsersAsync();
    }
}
