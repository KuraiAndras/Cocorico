using MediatR;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Users
{
    public sealed class GetAllUsersForAdmin : IRequest<ICollection<UserForAdminPage>>
    {
    }
}
