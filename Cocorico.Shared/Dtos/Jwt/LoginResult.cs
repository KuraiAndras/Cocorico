using System.Collections.Generic;

namespace Cocorico.Shared.Dtos.Jwt
{
    public class LoginResult
    {
        public JwtModel Jwt { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
