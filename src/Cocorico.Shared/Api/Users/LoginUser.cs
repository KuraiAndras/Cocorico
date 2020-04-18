using MediatR;

namespace Cocorico.Shared.Api.Users
{
    public sealed class LoginUser : LoginDetails, IRequest
    {
    }
}
