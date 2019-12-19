using Cocorico.Shared.Dtos.Authentication;

namespace Cocorico.Application.Users.Queries.GetClaims
{
    public sealed class GetUserClaimsByNameQuery : QueryDtoBase<UserNameDto, ClaimsDto>
    {
        public GetUserClaimsByNameQuery(UserNameDto dto) : base(dto)
        {
        }
    }
}