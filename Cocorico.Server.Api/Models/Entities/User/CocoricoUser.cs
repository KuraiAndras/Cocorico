using Microsoft.AspNetCore.Identity;

namespace Cocorico.Server.Api.Models.Entities.User
{
    public class CocoricoUser : IdentityUser, IDbEntity<string>
    {
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}
