using Cocorico.Shared.Dtos.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.ViewModels.User
{
    public interface IUsersViewModel
    {
        IReadOnlyList<UserForAdminPage> UsersList { get; }

        void GoToEdit(string userId);
        Task LoadUsersAsync();
    }
}
