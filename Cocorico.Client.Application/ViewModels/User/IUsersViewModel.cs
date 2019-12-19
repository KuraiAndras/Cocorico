using System.Collections.Generic;
using System.Threading.Tasks;
using Cocorico.Shared.Dtos.User;

namespace Cocorico.Client.Application.ViewModels.User
{
    public interface IUsersViewModel
    {
        IReadOnlyList<UserForAdminPage> UsersList { get; }

        void GoToEdit(string userId);
        Task LoadUsersAsync();
    }
}
