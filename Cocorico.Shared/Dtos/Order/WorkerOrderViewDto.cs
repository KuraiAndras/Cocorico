using Cocorico.Shared.Dtos.Sandwich;
using Cocorico.Shared.Helpers;
using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Order
{
    public class OrderWorkerViewDto
    {
        public int Id { get; set; }

        public string UserName { get; set; }
        public IEnumerable<SandwichDto> Sandwiches { get; set; }
        public OrderState State { get; set; }
    }
}
