using Cocorico.Shared.Dtos.Authentication;
using MediatR;

namespace Cocorico.Application.Users.Queries.GetClaims
{
    public sealed class GetUserClaimsByNameQuery : IRequest<ClaimsDto>
    {
        public GetUserClaimsByNameQuery(UserNameDto dto) => Dto = dto;

        public UserNameDto Dto { get; }
    }
}