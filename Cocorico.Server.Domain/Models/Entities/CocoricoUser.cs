using Microsoft.AspNetCore.Identity;

namespace Cocorico.Server.Domain.Models.Entities
{
    public class CocoricoUser : IdentityUser, IDbEntity<string>
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
