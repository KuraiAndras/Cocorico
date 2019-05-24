using Cocorico.Shared.Dtos.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Shared.Services
{
    public interface IUserService
    {
        Task<UserForAdminPage> GetUserForAdminPageAsync(string userId);
        Task<IEnumerable<UserForAdminPage>> GetAllUsersForAdminPageAsync();
    }
}
