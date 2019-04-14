using System.Collections.Generic;
using System.Threading.Tasks;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Services.Helpers;

namespace Cocorico.Shared.Services
{
    public interface IUserService
    {
        Task<IServiceResult<UserForAdminPage>> GetUserForAdminPageAsync(string userId);
        Task<IServiceResult<IEnumerable<UserForAdminPage>>> GetAllUsersForAdminPageAsync();
    }
}
