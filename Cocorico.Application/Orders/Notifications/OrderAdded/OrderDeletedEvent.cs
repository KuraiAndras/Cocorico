namespace Cocorico.Application.Orders.Notifications.OrderAdded
{
    public sealed class OrderDeletedEvent : EventDtoBase<int>
    {
        public OrderDeletedEvent(int dto)
            : base(dto)
        {
        }
    }
}
