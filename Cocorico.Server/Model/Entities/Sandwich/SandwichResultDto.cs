namespace Cocorico.Server.Model.Entities.Sandwich
{
    public class SandwichResultDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
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
