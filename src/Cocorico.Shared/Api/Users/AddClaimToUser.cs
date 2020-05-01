using MediatR;

namespace Cocorico.Shared.Api.Users
{
    public sealed class AddClaimToUser : UserClaimRequest, IRequest
    {
    }
}
