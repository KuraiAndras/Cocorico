using Cocorico.Shared.Identity;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cocorico.Application.Users.Services
{
    public class ClaimService : IClaimService
    {
        public Task<List<Claim>> GetBasicUserClaimsAsync() =>
            Task.FromResult(new List<Claim>
            {
                new Claim(ClaimTypes.Role, ApplicationClaims.User, ClaimValueTypes.String),
                new Claim(ClaimTypes.Role, ApplicationClaims.Customer, ClaimValueTypes.String),
            });
    }
}