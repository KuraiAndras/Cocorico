using Cocorico.Shared.Dtos.Opening;
using MediatR;

namespace Cocorico.Application.Openings.Commands.AddOpening
{
    public sealed class AddOpeningCommand : IRequest
    {
        public AddOpeningCommand(AddOpeningDto dto) => Dto = dto;

        public AddOpeningDto Dto { get; }
    }
}
