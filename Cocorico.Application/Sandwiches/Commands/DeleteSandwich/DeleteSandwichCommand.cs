using MediatR;

namespace Cocorico.Application.Sandwiches.Commands.DeleteSandwich
{
    public sealed class DeleteSandwichCommand : IRequest
    {
        public DeleteSandwichCommand(int id) => Id = id;

        public int Id { get; }
    }
}
