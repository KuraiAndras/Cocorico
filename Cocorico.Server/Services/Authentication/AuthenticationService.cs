using Cocorico.Server.Models;
using Cocorico.Server.Models.Entities.User;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cocorico.Server.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly CocoricoDbContext _cocoricoDbContext;
        private readonly UserManager<CocoricoUser> _userManager;
        private readonly SignInManager<CocoricoUser> _signInManager;

        public AuthenticationService(
            UserManager<CocoricoUser> userManager,
            CocoricoDbContext cocoricoDbContext,
            SignInManager<CocoricoUser> signInManager)
        {
            _userManager = userManager;
            _cocoricoDbContext = cocoricoDbContext;
            _signInManager = signInManager;
        }

        public async Task<IServiceResult> RegisterAsync(RegisterDetails model)
        {
            var userIdentity = model.MapTo(m => new CocoricoUser { UserName = m.Name });

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            //TODO: Better exception
            if (!result.Succeeded) return new Fail(new UnexpectedException());

            var customerClaim = new List<Claim>
            {
                new Claim(ClaimTypes.Role, Claims.User, ClaimValueTypes.String),
                new Claim(ClaimTypes.Role, Claims.Customer, ClaimValueTypes.String),
                new Claim(ClaimTypes.Role, Claims.Admin, ClaimValueTypes.String) //TODO: Do not add admin in register
            };

            var claimResult = await _userManager.AddClaimsAsync(userIdentity, customerClaim);

            //TODO: Better exception
            if (!claimResult.Succeeded) return new Fail(new UnexpectedException());

            await _cocoricoDbContext.SaveChangesAsync();

            return new Success();
        }

        public async Task<IServiceResult<LoginResult>> LoginAsync(LoginDetails model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return new Fail<LoginResult>(new EntityNotFoundException());

            var login = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
            if (login != SignInResult.Success) return new Fail<LoginResult>(new UnexpectedException()); //TODO: Better exception

            await _userManager.UpdateAsync(user);

            await _cocoricoDbContext.SaveChangesAsync();

            var claims = await _userManager.GetClaimsAsync(user);
            var result = new LoginResult { Claims = claims.Select(c => c.Value) };

            return new Success<LoginResult>(result);
        }

        public async Task<IServiceResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return new Success();
        }
    }
}
