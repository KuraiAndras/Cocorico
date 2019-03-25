using Microsoft.AspNetCore.Identity;

namespace Cocorico.RazorComponents.Models.Entities.User
{
    public class CocoricoUser : IdentityUser
    {
        public string Name { get; set;}
    }
}
