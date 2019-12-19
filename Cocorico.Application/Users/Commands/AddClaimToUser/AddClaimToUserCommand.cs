using Cocorico.Shared.Dtos.Authentication;

namespace Cocorico.Application.Users.Commands.AddClaimToUser
{
    public sealed class AddClaimToUserCommand : CommandDtoBase<UserClaimRequest>
    {
        public AddClaimToUserCommand(UserClaimRequest dto) : base(dto)
        {
        }
    }
}
