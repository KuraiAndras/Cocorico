using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Cocorico.DAL.Models.Entities
{
    public class CocoricoUser : IdentityUser, IDbEntity<string>
    {
        public string Name { get; set; } = null!;
        public ICollection<UserSandwichOrder> UserSandwichOrders { get; set; } = null!;
        public bool IsDeleted { get; set; }
    }
}
