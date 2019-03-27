using Cocorico.Server.Models.Entities.Sandwich;
using Cocorico.Shared.Dtos.Sandwich;

namespace Cocorico.Server.Extensions
{
    public static class DtoExtension
    {
        public static Sandwich ToSandwich(this NewSandwichDto newSandwichDto) =>
            new Sandwich
            {
                Id = newSandwichDto.Id,
                Name = newSandwichDto.Name,
            };

        public static SandwichResultDto ToSandwichResultDto(this Sandwich sandwich) =>
            new SandwichResultDto
            {
                Id = sandwich.Id,
                Name = sandwich.Name,
            };
    }
}
