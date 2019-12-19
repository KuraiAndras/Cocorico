namespace Cocorico.Application.Orders.Notifications.OrderAdded
{
    // TODO: explicit dto class
    public sealed class OrderDeletedEvent : EventDtoBase<int>
    {
        public OrderDeletedEvent(int dto) : base(dto)
        {
        }
    }
}