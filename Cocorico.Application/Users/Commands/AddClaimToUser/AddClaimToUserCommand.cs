using Cocorico.Shared.Dtos.Authentication;
using MediatR;

namespace Cocorico.Application.Users.Commands.AddClaimToUser
{
    public sealed class AddClaimToUserCommand : IRequest
    {
        public AddClaimToUserCommand(UserClaimRequest dto) => Dto = dto;

        public UserClaimRequest Dto { get; }
    }
}
