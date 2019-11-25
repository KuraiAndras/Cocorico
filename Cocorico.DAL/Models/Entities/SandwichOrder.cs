namespace Cocorico.DAL.Models.Entities
{
    public class SandwichOrder : IDbEntity
    {
        public int SandwichId { get; set; }
        public Sandwich Sandwich { get; set; } = null!;

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
