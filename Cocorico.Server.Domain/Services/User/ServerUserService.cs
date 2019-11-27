using Cocorico.DAL.Models;
using Cocorico.DAL.Models.Entities;
using Cocorico.Server.Domain.Services.ServiceBase;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Exceptions;
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

        public async Task<UserForAdminPage> GetUserForAdminPageAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException($"Cant find user with id:{userId}");

            var userClaims = await _userManager.GetClaimsAsync(user);

            return user.MapTo(_ => new UserForAdminPage { Claims = userClaims.Select(c => c.Value).ToList() });
        }

        public async Task<ICollection<UserForAdminPage>> GetAllUsersForAdminPageAsync()
        {
            var users = await Context.Users.ToListAsync() ?? throw new UnexpectedException();

            var usersForAdminPage = new List<UserForAdminPage>();
            foreach (var cocoricoUser in users)
            {
                var claims = (await _userManager.GetClaimsAsync(cocoricoUser)).Select(c => c.Value);

                usersForAdminPage.Add(cocoricoUser.MapTo(_ => new UserForAdminPage { Claims = claims.ToList() }));
            }

            return usersForAdminPage;
        }
    }
}
