using Cocorico.Shared.Dtos.Opening;

namespace Cocorico.Application.Openings.Commands.UpdateOpening
{
    public sealed class UpdateOpeningCommand : CommandDtoBase<OpeningDto>
    {
        public UpdateOpeningCommand(OpeningDto dto) : base(dto)
        {
        }
    }
}
