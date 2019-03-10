using Microsoft.AspNetCore.Identity;

namespace Cocorico.Server.Model
{
    public class CocoricoUser : IdentityUser
    {
        public string Name { get; set;}
    }
}
