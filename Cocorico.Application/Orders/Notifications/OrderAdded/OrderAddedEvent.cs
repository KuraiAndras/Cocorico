using Cocorico.Shared.Dtos.Orders;

namespace Cocorico.Application.Orders.Notifications.OrderAdded
{
    public sealed class OrderAddedEvent : EventDtoBase<WorkerOrderViewDto>
    {
        public OrderAddedEvent(WorkerOrderViewDto dto)
            : base(dto)
        {
        }
    }
}