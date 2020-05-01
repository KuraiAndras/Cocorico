using MediatR;

namespace Cocorico.Shared.Api.Users
{
    public sealed class GetUserForAdminQuery : IRequest<UserForAdminPage>
    {
        public string UserId { get; set; } = string.Empty;
    }
}
