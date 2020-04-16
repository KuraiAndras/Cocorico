using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Dtos.User;

namespace Cocorico.Application.Users.Queries.GetUserForAdmin
{
    public sealed class GetUserForAdminQuery : QueryDtoBase<UserIdDto, UserForAdminPage>
    {
        public GetUserForAdminQuery(UserIdDto dto)
            : base(dto)
        {
        }
    }
}
