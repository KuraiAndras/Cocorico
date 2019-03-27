using Microsoft.AspNetCore.Identity;

namespace Cocorico.Server.Models.Entities.User
{
    public class CocoricoUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
