using Cocorico.Shared.Dtos.Order;

namespace Cocorico.Application.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommand : CommandDtoBase<UpdateOrderDto>
    {
        public UpdateOrderCommand(UpdateOrderDto dto) : base(dto)
        {
        }
    }
}
