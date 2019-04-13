namespace Cocorico.Shared.Helpers
{
    public static class Urls
    {
        public static class Server
        {
            public const string Login = "api/authentication/login";
            public const string Register = "api/authentication/register";
            public const string Logout = "api/authentication/logout";
            public const string SandwichBase = "api/sandwich";
            public const string AddClaimToUser = "api/authentication/addClaimToUser";
        }

        public static class Client
        {
            public const string Home = "/";
            public const string Login = "login";
            public const string Register = "register";
            public const string Logout = "logout";
            public const string GetAllSandwich = "sandwiches";
            public const string AddSandwich = "addSandwich";
            public const string EditSandwich = "sandwiches/edit";
        }
    }
}
