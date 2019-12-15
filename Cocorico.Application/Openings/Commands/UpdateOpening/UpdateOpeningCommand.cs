using Cocorico.Shared.Dtos.Opening;
using MediatR;

namespace Cocorico.Application.Openings.Commands.UpdateOpening
{
    public sealed class UpdateOpeningCommand : IRequest
    {
        public UpdateOpeningCommand(OpeningDto dto) =>
            Dto = dto;

        public OpeningDto Dto { get; }
    }
}
