using MediatR;

namespace Cocorico.Shared.Api.Users
{
    public sealed class RemoveClaimFromUser : UserClaimRequest, IRequest
    {
    }
}
