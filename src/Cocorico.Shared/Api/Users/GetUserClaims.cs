using Cocorico.Shared.Api.Authentication;
using MediatR;

namespace Cocorico.Shared.Api.Users
{
    public sealed class GetUserClaims : IRequest<ClaimsDto>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
