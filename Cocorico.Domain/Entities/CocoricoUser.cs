using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Cocorico.Domain.Entities
{
    public sealed class CocoricoUser : IdentityUser
    {
        public string Name { get; set; } = null!;
        public ICollection<Order> Orders { get; } = new List<Order>();
    }
}
