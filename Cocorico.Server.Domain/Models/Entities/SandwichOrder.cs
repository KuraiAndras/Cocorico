namespace Cocorico.Server.Domain.Models.Entities
{
    public class SandwichOrder
    {
        public int SandwichId { get; set; }
        public Sandwich Sandwich { get; set; } = null!;

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}
