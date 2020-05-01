using Cocorico.Shared.Api.Authentication;
using MediatR;

namespace Cocorico.Shared.Api.Users
{
    public sealed class GetUserClaimsByName : IRequest<ClaimsDto>
    {
        public string UserName { get; set; } = string.Empty;
    }
}
