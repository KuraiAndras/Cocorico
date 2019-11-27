using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Order
{
    public class AddOrderDto
    {
        public string UserId { get; set; } = string.Empty;
        public IList<SandwichDto> Sandwiches { get; set; } = new List<SandwichDto>();
        public string CustomerId { get; set; } = string.Empty;
    }
}
