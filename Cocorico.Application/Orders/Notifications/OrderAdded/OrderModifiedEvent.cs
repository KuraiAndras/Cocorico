using Cocorico.Shared.Dtos.Order;

namespace Cocorico.Application.Orders.Notifications.OrderAdded
{
    public sealed class OrderModifiedEvent : EventDtoBase<WorkerOrderViewDto>
    {
        public OrderModifiedEvent(WorkerOrderViewDto dto) : base(dto)
        {
        }
    }
}