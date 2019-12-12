using AutoMapper;
using Cocorico.Domain.Entities;
using Cocorico.Persistence;
using Cocorico.Shared.Dtos.User;
using Cocorico.Shared.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.User
{
    public class ServerUserService : IServerUserService
    {
        private readonly CocoricoDbContext _context;
        private readonly UserManager<CocoricoUser> _userManager;
        private readonly IMapper _mapper;

        public ServerUserService(
            CocoricoDbContext context,
            UserManager<CocoricoUser> userManager,
            IMapper mapper)
        {
            _context = context;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<UserForAdminPage> GetUserForAdminPageAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId) ?? throw new EntityNotFoundException($"Cant find user with id:{userId}");

            var userClaims = await _userManager.GetClaimsAsync(user);

            var userForAdminPage = _mapper.Map<UserForAdminPage>(user);
            userForAdminPage.Claims = userClaims.Select(c => c.Value).ToList();

            return userForAdminPage;
        }

        public async Task<ICollection<UserForAdminPage>> GetAllUsersForAdminPageAsync()
        {
            var users = await _context.Users.ToListAsync() ?? throw new UnexpectedException();

            var usersForAdminPage = new List<UserForAdminPage>();
            foreach (var cocoricoUser in users)
            {
                var claims = (await _userManager.GetClaimsAsync(cocoricoUser)).Select(c => c.Value).ToList();

                var userForAdminPage = _mapper.Map<UserForAdminPage>(cocoricoUser);
                userForAdminPage.Claims = claims;

                usersForAdminPage.Add(userForAdminPage);
            }

            return usersForAdminPage;
        }
    }
}
