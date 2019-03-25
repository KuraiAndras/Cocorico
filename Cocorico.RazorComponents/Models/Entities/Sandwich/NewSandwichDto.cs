using System;

namespace Cocorico.RazorComponents.Models.Entities.Sandwich
{
    public class NewSandwichDto : IHashAssertable
    {
        public int Id {get;set;}
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
        public static Sandwich ToSandwich(this NewSandwichDto newSandwichDto) =>
            new Sandwich
            {
                Id = newSandwichDto.Id,
                Name = newSandwichDto.Name,
            };
    }
}
