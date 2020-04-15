using System.Collections.Generic;

namespace Cocorico.Shared.Identity
{
    public static class ApplicationClaims
    {
        public const string User = "User";
        public const string Customer = "Customer";
        public const string Admin = "Admin";
        public const string Worker = "Worker";

        public static readonly IEnumerable<string> ClaimsCollection = new[] { User, Customer, Admin, Worker };
    }
}
