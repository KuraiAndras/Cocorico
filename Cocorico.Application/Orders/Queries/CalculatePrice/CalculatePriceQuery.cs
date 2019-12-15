using Cocorico.Shared.Dtos.Order;
using MediatR;

namespace Cocorico.Application.Orders.Queries.CalculatePrice
{
    public sealed class CalculatePriceQuery : IRequest<int>
    {
        public CalculatePriceQuery(AddOrderDto dto) => Dto = dto;

        public AddOrderDto Dto { get; }
    }
}
