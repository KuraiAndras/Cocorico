namespace Cocorico.DAL.Models.Entities
{
    public class UserOrder : IDbEntity
    {
        public string UserId { get; set; } = null!;
        public CocoricoUser User { get; } = null!;

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public bool IsDeleted { get; set; }
    }
}