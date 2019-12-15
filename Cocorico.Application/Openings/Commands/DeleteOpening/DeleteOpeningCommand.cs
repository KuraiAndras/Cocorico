using MediatR;

namespace Cocorico.Application.Openings.Commands.DeleteOpening
{
    public sealed class DeleteOpeningCommand : IRequest
    {
        public DeleteOpeningCommand(int id) => Id = id;

        public int Id { get; }
    }
}
