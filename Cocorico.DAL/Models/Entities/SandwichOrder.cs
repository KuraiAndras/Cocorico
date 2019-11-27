namespace Cocorico.DAL.Models.Entities
{
    public class SandwichOrder : IDbEntity<int>
    {
        public int Id { get; set; }

        public int SandwichId { get; set; }
        public Sandwich Sandwich { get; set; } = null!;

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}
