using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.User
{
    public class UserForAdminPage
    {
        public string Id { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public ICollection<string> Claims { get; set; } = new List<string>();
    }
}
