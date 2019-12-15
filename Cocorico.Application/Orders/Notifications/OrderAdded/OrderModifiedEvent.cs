using Cocorico.Shared.Dtos.Order;
using MediatR;

namespace Cocorico.Application.Orders.Notifications.OrderAdded
{
    public sealed class OrderModifiedEvent : INotification
    {
        public OrderModifiedEvent(WorkerOrderViewDto dto) => Dto = dto;

        public WorkerOrderViewDto Dto { get; }
    }
}