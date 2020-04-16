using System;
using System.Collections.Generic;

namespace Cocorico.Persistence.Entities
{
    public sealed class Opening
    {
        public int Id { get; set; }
        public ICollection<Order> Orders { get; } = new List<Order>();
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
