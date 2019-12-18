using Cocorico.Shared.Dtos.Authentication;
using MediatR;

namespace Cocorico.Application.Users.Queries.GetClaims
{
    public sealed class GetClaimsQuery : IRequest<ClaimsDto>
    {
        public GetClaimsQuery(UserNameDto dto) => Dto = dto;

        public UserNameDto Dto { get; }
    }
}
