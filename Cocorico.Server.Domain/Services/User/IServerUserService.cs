using Cocorico.Shared.Dtos.User;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.User
{
    public interface IServerUserService
    {
        Task<UserForAdminPage> GetUserForAdminPageAsync(string userId);
        Task<IEnumerable<UserForAdminPage>> GetAllUsersForAdminPageAsync();
    }
}
