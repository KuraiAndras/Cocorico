using MediatR;
using System.Collections.Generic;

namespace Cocorico.Shared.Api.Orders
{
    public sealed class GetAllOrderForCustomer : IRequest<ICollection<CustomerViewOrderDto>>
    {
        public string CustomerId { get; set; } = string.Empty;
    }
}
