using Cocorico.Shared.Dtos.Order;
using MediatR;

namespace Cocorico.Application.Orders.Commands.UpdateOrder
{
    public sealed class UpdateOrderCommand : IRequest
    {
        public UpdateOrderCommand(UpdateOrderDto dto) => Dto = dto;

        public UpdateOrderDto Dto { get; }
    }
}
