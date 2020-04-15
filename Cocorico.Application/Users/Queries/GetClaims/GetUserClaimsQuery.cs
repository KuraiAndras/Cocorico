using Cocorico.Shared.Dtos.Authentication;

namespace Cocorico.Application.Users.Queries.GetClaims
{
    public sealed class GetUserClaimsQuery : QueryDtoBase<UserIdDto, ClaimsDto>
    {
        public GetUserClaimsQuery(UserIdDto dto)
            : base(dto)
        {
        }
    }
}
