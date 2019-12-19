using Cocorico.Shared.Dtos.Authentication;

namespace Cocorico.Application.Users.Commands.RemoveClaimFromUser
{
    public sealed class RemoveClaimFromUserCommand : CommandDtoBase<UserClaimRequest>
    {
        public RemoveClaimFromUserCommand(UserClaimRequest dto) : base(dto)
        {
        }
    }
}
