using System.Collections.Generic;
using System.Security.Claims;

namespace Cocorico.Shared.Dtos.Authentication
{
    public sealed class ClaimsDto
    {
        public ICollection<Claim> Claims { get; set; } = new List<Claim>();
    }
}
