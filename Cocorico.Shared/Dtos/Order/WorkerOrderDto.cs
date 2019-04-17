using Cocorico.Shared.Dtos.Sandwich;
using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Order
{
    public class WorkerOrderDto
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public IEnumerable<SandwichResultDto> Sandwiches { get; set; }
        public int Price { get; set; }
    }
}
