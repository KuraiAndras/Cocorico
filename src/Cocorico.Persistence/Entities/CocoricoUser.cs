using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Cocorico.Persistence.Entities
{
    public sealed class CocoricoUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
