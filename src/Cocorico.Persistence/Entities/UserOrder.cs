namespace Cocorico.Persistence.Entities
{
    public sealed class UserOrder
    {
        public string UserId { get; set; } = null!;
        public CocoricoUser User { get; } = null!;

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
    }
}