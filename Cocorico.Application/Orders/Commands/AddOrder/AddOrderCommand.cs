using Cocorico.Shared.Dtos.Order;
using MediatR;

namespace Cocorico.Application.Orders.Commands.AddOrder
{
    public sealed class AddOrderCommand : IRequest
    {
        public AddOrderCommand(AddOrderDto dto) => Dto = dto;

        public AddOrderDto Dto { get; }
    }
}
