using MediatR;

namespace Cocorico.Application.Orders.Notifications.OrderAdded
{
    public sealed class OrderDeletedEvent : INotification
    {
        public OrderDeletedEvent(int id) => Id = id;

        public int Id { get; }
    }
}