using Cocorico.Server.Domain.Extensions;
using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Models.Entities;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
using Cocorico.Shared.Services.Helpers;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cocorico.Server.Domain.Services.Authentication
{
    public class ServerCocoricoAuthenticationService : IServerCocoricoAuthenticationService
    {
        private readonly CocoricoDbContext _cocoricoDbContext;
        private readonly UserManager<CocoricoUser> _userManager;
        private readonly SignInManager<CocoricoUser> _signInManager;

        public ServerCocoricoAuthenticationService(
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
            var userIdentity = model.MapTo(m => new CocoricoUser { UserName = m.Email });

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            //TODO: Better exception
            if (!result.Succeeded) return new Fail(new UnexpectedException());

            var customerClaim = new[]
            {
                new Claim(ClaimTypes.Role, Claims.User, ClaimValueTypes.String),
                new Claim(ClaimTypes.Role, Claims.Customer, ClaimValueTypes.String),
            };

            var claimResult = await _userManager.AddClaimsAsync(userIdentity, customerClaim);

            if (!claimResult.Succeeded) return new Fail();

            var saveResult = await _cocoricoDbContext.TrySaveChangesAsync();

            return saveResult;
        }

        public async Task<IServiceResult<LoginResult>> LoginAsync(LoginDetails model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return new Fail<LoginResult>(new EntityNotFoundException());

            var login = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
            if (login != SignInResult.Success) return new Fail<LoginResult>(); //TODO: Better exception

            await _userManager.UpdateAsync(user);

            var saveResult = await _cocoricoDbContext.TrySaveChangesAsync();
            if (saveResult is Fail) return new Fail<LoginResult>();

            var claims = await _userManager.GetClaimsAsync(user);
            var result = new LoginResult { Claims = claims.Select(c => c.Value) };

            return new Success<LoginResult>(result);
        }

        public async Task<IServiceResult> LogoutAsync()
        {
            await _signInManager.SignOutAsync();

            return new Success();
        }

        public async Task<IServiceResult> AddClaimToUserAsync(UserClaimRequest userClaimRequest)
        {
            var user = await _userManager.FindByIdAsync(userClaimRequest.UserId);
            if (user is null) return new Fail(new EntityNotFoundException());

            //Sign user out
            await _userManager.UpdateSecurityStampAsync(user);

            var userClaims = await _userManager.GetClaimsAsync(user);
            var newClaim = new Claim(ClaimTypes.Role, userClaimRequest.CocoricoClaim.ClaimValue, ClaimValueTypes.String);
            var oldClaim = userClaims.SingleOrDefault(c => c.Value.Equals(newClaim.Value));

            if (!(oldClaim is null))
            {
                var removeClaimResult = await _userManager.RemoveClaimAsync(user, oldClaim);
                if (!removeClaimResult.Succeeded) return new Fail();
            }

            var addClaimResult = await _userManager.AddClaimAsync(user, newClaim);
            if (!addClaimResult.Succeeded) return new Fail();

            var saveResult = await _cocoricoDbContext.TrySaveChangesAsync();

            return saveResult;
        }

        public async Task<IServiceResult> RemoveClaimFromUserAsync(UserClaimRequest userClaimRequest)
        {
            var user = await _userManager.FindByIdAsync(userClaimRequest.UserId);
            if (user is null) return new Fail(new EntityNotFoundException());

            //Sign user out
            await _userManager.UpdateSecurityStampAsync(user);

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claimToRemove = new Claim(ClaimTypes.Role, userClaimRequest.CocoricoClaim.ClaimValue, ClaimValueTypes.String);
            var oldClaim = userClaims.SingleOrDefault(c => c.Value.Equals(claimToRemove.Value));

            if (oldClaim is null) return new Fail(new InvalidCommandException());

            var removeClaimResult = await _userManager.RemoveClaimAsync(user, claimToRemove);
            if (!removeClaimResult.Succeeded) return new Fail();

            var saveResult = await _cocoricoDbContext.TrySaveChangesAsync();

            return saveResult;
        }
    }
}
