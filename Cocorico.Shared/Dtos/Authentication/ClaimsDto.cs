using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Authentication
{
    public sealed class ClaimsDto
    {
        public ICollection<ClaimDto> Claims { get; set; } = new List<ClaimDto>();
    }
}
