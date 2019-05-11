using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Order
{
    public class OrderAddDto
    {
        public string UserId { get; set; }
        public IEnumerable<SandwichResultDto> Sandwiches { get; set; }
        public string CustomerId { get; set; }
    }
}
