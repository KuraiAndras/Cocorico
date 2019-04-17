using System.Collections.Generic;
using Cocorico.Shared.Dtos.Sandwich;

namespace Cocorico.Shared.Dtos.Order
{
    public class CustomerOrderDto
    {
        public int Id { get; set; }
        public IEnumerable<SandwichResultDto> Sandwiches { get; set; }
        public int Price { get; set; }
    }
}
