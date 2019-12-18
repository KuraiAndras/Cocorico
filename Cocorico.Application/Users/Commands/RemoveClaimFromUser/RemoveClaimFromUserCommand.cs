using Cocorico.Shared.Dtos.Authentication;
using MediatR;

namespace Cocorico.Application.Users.Commands.RemoveClaimFromUser
{
    public sealed class RemoveClaimFromUserCommand : IRequest
    {
        public RemoveClaimFromUserCommand(UserClaimRequest dto) => Dto = dto;

        public UserClaimRequest Dto { get; }
    }
}
