using Cocorico.Shared.Dtos.Order;
using MediatR;
using System.Collections.Generic;

namespace Cocorico.Application.Orders.Queries.GetAllOrderForCustomer
{
    public sealed class GetAllOrderForCustomerQuery : IRequest<ICollection<CustomerViewOrderDto>>
    {
        public GetAllOrderForCustomerQuery(string customerId) =>
            CustomerId = customerId;

        public string CustomerId { get; }
    }
}
