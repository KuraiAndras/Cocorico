using Cocorico.Shared.Dtos.Order;

namespace Cocorico.Application.Orders.Commands.AddOrder
{
    public sealed class AddOrderCommand : CommandDtoBase<AddOrderDto>
    {
        public AddOrderCommand(AddOrderDto dto) : base(dto)
        {
        }
    }
}
