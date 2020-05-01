using MediatR;

namespace Cocorico.Shared.Api.Orders
{
    public sealed class DeleteOrder : IRequest
    {
        public int Id { get; set; }
    }
}
