using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Cocorico.Server.Domain.Models.Entities
{
    public class CocoricoUser : IdentityUser, IDbEntity<string>
    {
        public string Name { get; set; } = null!;
        public ICollection<Order> Orders { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
