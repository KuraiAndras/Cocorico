using Cocorico.Shared.Api.Orders;

namespace Cocorico.Application.Orders.Notifications
{
    public sealed class OrderAddedEvent : EventDtoBase<WorkerOrderViewDto>
    {
        public OrderAddedEvent(WorkerOrderViewDto dto)
            : base(dto)
        {
        }
    }
}
