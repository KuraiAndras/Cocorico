using Cocorico.Shared.Dtos.Sandwich;
using MediatR;

namespace Cocorico.Application.Sandwiches.Commands.UpdateSandwich
{
    public sealed class UpdateSandwichCommand : IRequest
    {
        public UpdateSandwichCommand(SandwichDto dto) =>
            Dto = dto;

        public SandwichDto Dto { get; }
    }
}
