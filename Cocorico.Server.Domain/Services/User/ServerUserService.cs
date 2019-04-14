using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Models.Entities.User;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.User
{
    public class ServerUserService : EntityServiceBase<CocoricoUser, string>, IServerUserService
    {
        private readonly UserManager<CocoricoUser> _userManager;

        public ServerUserService(CocoricoDbContext context, UserManager<CocoricoUser> userManager) : base(context) => _userManager = userManager;

        public async Task<IServiceResult<IEnumerable<UserForAdminPage>>> GetAllUsersForAdminPageAsync()
        {
            var users = await Context.Users.ToListAsync();

            var usersForAdminPage = new List<UserForAdminPage>();
            foreach (var cocoricoUser in users)
            {
                var claims = (await _userManager.GetClaimsAsync(cocoricoUser)).Select(c => c.Value);

                usersForAdminPage.Add(cocoricoUser.MapTo(u => new UserForAdminPage { Claims = claims }));
            }

            return new Success<IEnumerable<UserForAdminPage>>(usersForAdminPage);
        }
    }
}
