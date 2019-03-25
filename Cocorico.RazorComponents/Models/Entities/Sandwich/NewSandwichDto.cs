namespace Cocorico.RazorComponents.Models.Entities.Sandwich
{
    public class NewSandwichDto
    {
        public string Name { get; set; }
    }

    public static partial class DtoExtension
    {
        public static Sandwich ToSandwich(this NewSandwichDto newSandwichDto) =>
            new Sandwich
            {
                Id = 0,
                Name = newSandwichDto.Name
            };
    }
}
