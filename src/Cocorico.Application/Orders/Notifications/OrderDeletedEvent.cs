namespace Cocorico.Application.Orders.Notifications
{
    public sealed class OrderDeletedEvent : EventDtoBase<int>
    {
        public OrderDeletedEvent(int dto)
            : base(dto)
        {
        }
    }
}
