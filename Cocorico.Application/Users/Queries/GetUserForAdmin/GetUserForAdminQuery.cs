using Cocorico.Shared.Dtos.Authentication;
using Cocorico.Shared.Dtos.User;
using MediatR;

namespace Cocorico.Application.Users.Queries.GetUserForAdmin
{
    public sealed class GetUserForAdminQuery : IRequest<UserForAdminPage>
    {
        public GetUserForAdminQuery(UserIdDto dto) => Dto = dto;

        public UserIdDto Dto { get; }
    }
}
