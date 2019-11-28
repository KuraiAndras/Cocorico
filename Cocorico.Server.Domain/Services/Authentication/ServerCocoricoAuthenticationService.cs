using AutoMapper;
using Cocorico.DAL.Models;
using Cocorico.DAL.Models.Entities;
using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Exceptions;
using Cocorico.Shared.Helpers;
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
        private readonly IMapper _mapper;

        public ServerCocoricoAuthenticationService(
            UserManager<CocoricoUser> userManager,
            CocoricoDbContext cocoricoDbContext,
            SignInManager<CocoricoUser> signInManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _cocoricoDbContext = cocoricoDbContext;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        public async Task RegisterAsync(RegisterDetails model)
        {
            var userIdentity = _mapper.Map<CocoricoUser>(model);

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            //TODO: Better exception
            if (!result.Succeeded) throw new UnexpectedException();

            var customerClaim = new[]
            {
                new Claim(ClaimTypes.Role, Claims.User, ClaimValueTypes.String),
                new Claim(ClaimTypes.Role, Claims.Customer, ClaimValueTypes.String),
            };

            var claimResult = await _userManager.AddClaimsAsync(userIdentity, customerClaim);

            if (!claimResult.Succeeded) throw new UnexpectedException();

            await _cocoricoDbContext.SaveChangesAsync();
        }

        public async Task<LoginResult> LoginAsync(LoginDetails model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email) ?? throw new EntityNotFoundException($"User not found with email: {model.Email}");

            var login = await _signInManager.PasswordSignInAsync(model.Email, model.Password, true, false);
            if (login != SignInResult.Success) throw new InvalidCredentialsException();

            await _userManager.UpdateAsync(user);

            await _cocoricoDbContext.SaveChangesAsync();

            var claims = await _userManager.GetClaimsAsync(user) ?? throw new UnexpectedException();

            return new LoginResult { Claims = claims.Select(c => c.Value).ToList() };
        }

        public async Task LogoutAsync() => await _signInManager.SignOutAsync();

        public async Task AddClaimToUserAsync(UserClaimRequest userClaimRequest)
        {
            var user = await _userManager.FindByIdAsync(userClaimRequest.UserId)
                       ?? throw new EntityNotFoundException($"User not found with id: {userClaimRequest.UserId}");

            //Sign user out
            await _userManager.UpdateSecurityStampAsync(user);

            var userClaims = await _userManager.GetClaimsAsync(user);
            var newClaim = new Claim(ClaimTypes.Role, userClaimRequest.CocoricoClaim.ClaimValue, ClaimValueTypes.String);
            var oldClaim = userClaims.SingleOrDefault(c => c.Value.Equals(newClaim.Value));

            if (!(oldClaim is null))
            {
                var removeClaimResult = await _userManager.RemoveClaimAsync(user, oldClaim);
                if (!removeClaimResult.Succeeded) throw new UnexpectedException();
            }

            var addClaimResult = await _userManager.AddClaimAsync(user, newClaim);
            if (!addClaimResult.Succeeded) throw new UnexpectedException();

            await _cocoricoDbContext.SaveChangesAsync();
        }

        public async Task RemoveClaimFromUserAsync(UserClaimRequest userClaimRequest)
        {
            var user = await _userManager.FindByIdAsync(userClaimRequest.UserId)
                       ?? throw new EntityNotFoundException($"User not found with id:{userClaimRequest.UserId}");

            //Sign user out
            await _userManager.UpdateSecurityStampAsync(user);

            var userClaims = await _userManager.GetClaimsAsync(user);

            var claimToRemove = new Claim(ClaimTypes.Role, userClaimRequest.CocoricoClaim.ClaimValue, ClaimValueTypes.String);
            var oldClaim = userClaims.SingleOrDefault(c => c.Value.Equals(claimToRemove.Value));

            if (oldClaim is null) throw new InvalidCommandException();

            var removeClaimResult = await _userManager.RemoveClaimAsync(user, claimToRemove);
            if (!removeClaimResult.Succeeded) throw new UnexpectedException();

            await _cocoricoDbContext.SaveChangesAsync();
        }
    }
}
