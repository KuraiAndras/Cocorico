using System;
using System.Collections.Generic;

namespace Cocorico.DAL.Models.Entities
{
    public class Opening : IDbEntity<int>
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public ICollection<Order> Orders { get; set; } = null!;
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}
