using Cocorico.Shared.Dtos.User;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Application.Users.Queries.GetUserForAdmin
{
    public sealed class GetAllUsersForAdminQuery : IRequest<ICollection<UserForAdminPage>>
    {
    }
}