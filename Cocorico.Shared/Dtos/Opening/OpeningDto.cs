using System;

namespace Cocorico.Shared.Dtos.Opening
{
    public class OpeningDto
    {
        public int Id { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
        public int NumberOfOrders { get; set; }
    }
}
