using Cocorico.Shared.Dtos.Openings;

namespace Cocorico.Application.Openings.Commands.AddOpening
{
    public sealed class AddOpeningCommand : CommandDtoBase<AddOpeningDto>
    {
        public AddOpeningCommand(AddOpeningDto dto)
            : base(dto)
        {
        }
    }
}
