using System.Collections.Generic;
using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;

namespace Cocorico.Shared.Dtos.Order
{
    public class CustomerViewOrderDto
    {
        public int Id { get; set; }
        public ICollection<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();
        public int Price { get; set; }
        public OrderState State { get; set; }
    }
}
