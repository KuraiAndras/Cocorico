using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Authentication
{
    public class LoginResult
    {
        public IEnumerable<string> Claims { get; set; }
    }
}
