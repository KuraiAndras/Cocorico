namespace Cocorico.Server.Model.Entities.Sandwich
{
    public class NewSandwichDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public static partial class DtoExtension
    {
        public static Sandwich ToSandwich(this NewSandwichDto newSandwichDto) =>
            new Sandwich
            {
                Id = newSandwichDto.Id,
                Name = newSandwichDto.Name
            };
    }
}
