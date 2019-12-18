using Cocorico.Shared.Dtos.Authentication;
using MediatR;

namespace Cocorico.Application.Users.Queries.GetClaims
{
    public sealed class GetUserClaimsQuery : IRequest<ClaimsDto>
    {
        public GetUserClaimsQuery(UserIdDto dto) => Dto = dto;

        public UserIdDto Dto { get; }
    }
}
