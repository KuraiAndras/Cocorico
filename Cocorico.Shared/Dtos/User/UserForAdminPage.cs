using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.User
{
    public class UserForAdminPage
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Claims { get; set; }
    }
}
