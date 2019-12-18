using Cocorico.Shared.Dtos.Authentication;
using MediatR;

namespace Cocorico.Application.Users.Commands.LoginUser
{
    public sealed class LoginUserCommand : IRequest
    {
        public LoginUserCommand(LoginDetails dto) => Dto = dto;

        public LoginDetails Dto { get; }
    }
}
