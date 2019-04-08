using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cocorico.Server.Models;
using Cocorico.Server.Models.Entities.User;
using Cocorico.Server.Models.NonDb;
using Cocorico.Server.Services.Jwt;
using Cocorico.Shared.Dtos.Jwt;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Cocorico.Server.Services.Authentication
{
    public class CustomAuthenticationService : ICustomAuthenticationService
    {
        private readonly CocoricoDbContext _cocoricoDbContext;
        private readonly UserManager<CocoricoUser> _userManager;
        private readonly SignInManager<CocoricoUser> _signInManager;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly JwtIssuerOptions _jwtOptions;

        public CustomAuthenticationService(
            UserManager<CocoricoUser> userManager,
            CocoricoDbContext cocoricoDbContext,
            SignInManager<CocoricoUser> signInManager,
            IJwtTokenService jwtTokenService,
            IOptions<JwtIssuerOptions> jwtOptions)
        {
            _userManager = userManager;
            _cocoricoDbContext = cocoricoDbContext;
            _signInManager = signInManager;
            _jwtTokenService = jwtTokenService;
            _jwtOptions = jwtOptions.Value;
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

            //TODO: Handle fail
            var roleResult = await _userManager.AddToRoleAsync(userIdentity, Verbs.CocoricoUser);
            if (!roleResult.Succeeded) return;

            await _cocoricoDbContext.SaveChangesAsync();
        }

        public async Task<LoginResult> LoginAsync(LoginDetails model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            //TODO: Handle fail

            var login = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);

            //TODO: Handle fail
            if (login != SignInResult.Success)
                return new LoginResult
                {
                    Jwt = new JwtModel
                    {
                        ExpiresIn = 0,
                        Token = "",
                        UserId = "",
                    },
                    Roles = new List<string>()
                };

            var identity = await GetClaimsIdentity(model.Email, model.Password);

            var roles = await _userManager.GetRolesAsync(user);

            var jwt = await GenerateJwt(identity, model.Email, roles);

            var result = new LoginResult
            {
                Jwt = jwt,
                Roles = roles,
            };

            await _userManager.UpdateAsync(user);

            await _cocoricoDbContext.SaveChangesAsync();

            return result;
        }

        private async Task<JwtModel> GenerateJwt(ClaimsIdentity identity, string userName, IList<string> roles)
        {
            var response = new JwtModel
            {
                UserId = identity.Claims.Single(c => c.Type == "id").Value,
                Token = await _jwtTokenService.GenerateEncodedToken(userName, identity, roles),
                ExpiresIn = (int)_jwtOptions.ValidFor.TotalSeconds,
            };
            return response;
        }

        private async Task<ClaimsIdentity> GetClaimsIdentity(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
                return null;

            // get the user to verify
            var userToVerify = await _userManager.FindByNameAsync(userName);

            if (userToVerify == null) return null;

            // check the credentials
            return await _userManager.CheckPasswordAsync(userToVerify, password)
                ? _jwtTokenService.GenerateClaimsIdentity(userName, userToVerify.Id)
                : null;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
