using Cocorico.Shared.Dtos.Order;
using System.Collections.Generic;

namespace Cocorico.Application.Orders.Queries.GetAllOrderForCustomer
{
    public sealed class GetAllOrderForCustomerQuery : QueryDtoBase<string, ICollection<CustomerViewOrderDto>>
    {
        public GetAllOrderForCustomerQuery(string dto) : base(dto)
        {
        }
    }
}
