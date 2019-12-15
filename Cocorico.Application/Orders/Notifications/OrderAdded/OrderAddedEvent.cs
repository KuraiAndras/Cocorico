using Cocorico.Shared.Dtos.Order;
using MediatR;

namespace Cocorico.Application.Orders.Notifications.OrderAdded
{
    public sealed class OrderAddedEvent : INotification
    {
        public OrderAddedEvent(WorkerOrderViewDto dto) => Dto = dto;

        public WorkerOrderViewDto Dto { get; }
    }
}