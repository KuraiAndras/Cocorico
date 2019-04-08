using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Cocorico.Server.Services.Jwt
{
    public interface IJwtTokenService
    {
        Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity, IList<string> roles);
        ClaimsIdentity GenerateClaimsIdentity(string userName, string id);
    }
}
