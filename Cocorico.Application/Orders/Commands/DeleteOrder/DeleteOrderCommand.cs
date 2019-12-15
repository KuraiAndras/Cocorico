using MediatR;

namespace Cocorico.Application.Orders.Commands.DeleteOrder
{
    public sealed class DeleteOrderCommand : IRequest
    {
        public DeleteOrderCommand(int id) => Id = id;

        public int Id { get; }
    }
}
