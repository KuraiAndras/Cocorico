using MediatR;

namespace Cocorico.Shared.Api.Sandwiches
{
    public sealed class DeleteSandwich : IRequest
    {
        public int SandwichId { get; set; }
    }
}
