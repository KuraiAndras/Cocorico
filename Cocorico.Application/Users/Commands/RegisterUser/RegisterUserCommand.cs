using Cocorico.Shared.Dtos.Authentication;
using MediatR;

namespace Cocorico.Application.Users.Commands.RegisterUser
{
    public sealed class RegisterUserCommand : IRequest
    {
        public RegisterUserCommand(RegisterDetails dto) => Dto = dto;

        public RegisterDetails Dto { get; }
    }
}
