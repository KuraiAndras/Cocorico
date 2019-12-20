using Cocorico.Shared.Dtos.Sandwich;

namespace Cocorico.Application.Sandwiches.Commands.AddSandwich
{
    public sealed class AddSandwichCommand : CommandDtoBase<SandwichAddDto>
    {
        public AddSandwichCommand(SandwichAddDto dto) : base(dto)
        {
        }
    }
}
