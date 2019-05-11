using System.Collections.Generic;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;

namespace Cocorico.Shared.Dtos.Order
{
    public class OrderCustomerViewDto
    {
        public int Id { get; set; }
        public IEnumerable<SandwichResultDto> Sandwiches { get; set; }
        public int Price { get; set; }
        public OrderState State { get; set; }
    }
}
