using Cocorico.Shared.Api.Orders;

namespace Cocorico.Application.Orders.Notifications
{
    public sealed class OrderModifiedEvent : EventDtoBase<WorkerOrderViewDto>
    {
        public OrderModifiedEvent(WorkerOrderViewDto dto)
            : base(dto)
        {
        }
    }
}
