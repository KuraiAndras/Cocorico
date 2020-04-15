using Cocorico.Shared.Dtos.Authentication;

namespace Cocorico.Application.Users.Commands.RegisterUser
{
    public sealed class RegisterUserCommand : CommandDtoBase<RegisterDetails>
    {
        public RegisterUserCommand(RegisterDetails dto)
            : base(dto)
        {
        }
    }
}
