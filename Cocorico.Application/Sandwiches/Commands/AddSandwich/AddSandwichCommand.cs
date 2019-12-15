using Cocorico.Shared.Dtos.Sandwich;
using MediatR;

namespace Cocorico.Application.Sandwiches.Commands.AddSandwich
{
    public sealed class AddSandwichCommand : IRequest
    {
        public AddSandwichCommand(SandwichAddDto sandwichAddDto) =>
            SandwichAddDto = sandwichAddDto;

        public SandwichAddDto SandwichAddDto { get; }
    }
}
