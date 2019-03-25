using System;

namespace Cocorico.RazorComponents.Models.Entities.Sandwich
{
    public class SandwichResultDto : IHashAssertable
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GetAssertHash()
        {
            var hashCode = new HashCode();

            hashCode.Add(Id);
            hashCode.Add(Name);

            return hashCode.ToHashCode();
        }
    }

    public static partial class DtoExtension
    {
        public static SandwichResultDto ToSandwichResultDto(this Sandwich sandwich) =>
            new SandwichResultDto
            {
                Id = sandwich.Id,
                Name = sandwich.Name,
            };
    }
}
