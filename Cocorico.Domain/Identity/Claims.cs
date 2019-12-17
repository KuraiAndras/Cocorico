using System.Collections.Generic;

namespace Cocorico.Domain.Identity
{
    public static class Claims
    {
        public static readonly IEnumerable<string> ClaimsCollection = new[] { User, Customer, Admin, Worker };
        public const string User = "User";
        public const string Customer = "Customer";
        public const string Admin = "Admin";
        public const string Worker = "Worker";
    }
}
