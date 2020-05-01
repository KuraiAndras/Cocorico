using Cocorico.Shared.Entities;
using MediatR;

namespace Cocorico.Shared.Api.Orders
{
    public class UpdateOrder : IRequest
    {
        public int OrderId { get; set; }
        public OrderState State { get; set; }
    }
}
