using Cocorico.Shared.Dtos.Sandwiches;

namespace Cocorico.Application.Sandwiches.Commands.UpdateSandwich
{
    public sealed class UpdateSandwichCommand : CommandDtoBase<SandwichDto>
    {
        public UpdateSandwichCommand(SandwichDto dto)
            : base(dto)
        {
        }
    }
}
