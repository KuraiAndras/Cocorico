using Cocorico.Shared.Dtos.Sandwich;

namespace Cocorico.Application.Sandwiches.Commands.UpdateSandwich
{
    public sealed class UpdateSandwichCommand : CommandDtoBase<SandwichDto>
    {
        public UpdateSandwichCommand(SandwichDto dto) : base(dto)
        {
        }
    }
}
