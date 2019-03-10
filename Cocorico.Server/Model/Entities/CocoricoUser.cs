using Microsoft.AspNetCore.Identity;

namespace Cocorico.Server.Model.Entities
{
    public class CocoricoUser : IdentityUser
    {
        public string Name { get; set;}
    }
}
