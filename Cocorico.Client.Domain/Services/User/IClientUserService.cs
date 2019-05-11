using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Services.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Client.Domain.Services.User
{
    public interface IClientUserService
    {
        Task<IServiceResult<UserForAdminPage>> GetUserForAdminPageAsync(string userId);
        Task<IServiceResult<IEnumerable<UserForAdminPage>>> GetAllUsersForAdminPageAsync();
    }
}
