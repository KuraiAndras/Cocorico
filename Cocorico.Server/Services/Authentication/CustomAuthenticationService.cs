using Cocorico.Server.Models;
using Cocorico.Server.Models.Entities.User;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Cocorico.Server.Services.Authentication
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly CocoricoDbContext _cocoricoDbContext;
        private readonly UserManager<CocoricoUser> _userManager;
        private readonly SignInManager<CocoricoUser> _signInManager;

        public CustomAuthenticationService(
            UserManager<CocoricoUser> userManager,
            CocoricoDbContext cocoricoDbContext,
            SignInManager<CocoricoUser> signInManager)
        {
            _userManager = userManager;
            _cocoricoDbContext = cocoricoDbContext;
            _signInManager = signInManager;
        }

        public async Task RegisterAsync(RegisterDetails model)
        {
            var userIdentity = new CocoricoUser
            {
                Name = model.Name,
                Email = model.Email,
                UserName = model.Email,
            };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            //TODO: Handle fail
            if (!result.Succeeded) return;

            var roleResult = await _userManager.AddToRoleAsync(userIdentity, Verbs.CocoricoUser);

            //TODO: Handle fail
            if (!roleResult.Succeeded) return;

            await _cocoricoDbContext.SaveChangesAsync();
        }

        public async Task<LoginResult> LoginAsync(LoginDetails model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            //TODO: Handle fail

            var login = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            //TODO: Handle fail
            if (login != SignInResult.Success) return new LoginResult { Roles = new List<string>() };

            var roles = await _userManager.GetRolesAsync(user);

            var result = new LoginResult { Roles = roles, };

            await _userManager.UpdateAsync(user);

            await _cocoricoDbContext.SaveChangesAsync();

            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
