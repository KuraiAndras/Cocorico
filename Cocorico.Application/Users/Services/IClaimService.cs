using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cocorico.Application.Users.Services
{
    public interface IClaimService
    {
        public Task<List<Claim>> GetBasicUserClaimsAsync();
    }
}
