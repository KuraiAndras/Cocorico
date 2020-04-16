using Cocorico.Shared.Dtos.Orders;

namespace Cocorico.Application.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommand : CommandDtoBase<UpdateOrderDto>
    {
        public UpdateOrderCommand(UpdateOrderDto dto)
            : base(dto)
        {
        }
    }
}
