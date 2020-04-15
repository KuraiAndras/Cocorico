using Cocorico.Shared.Dtos.Authentication;

namespace Cocorico.Application.Users.Commands.LoginUser
{
    public sealed class LoginUserCommand : CommandDtoBase<LoginDetails>
    {
        public LoginUserCommand(LoginDetails dto)
            : base(dto)
        {
        }
    }
}
