namespace Cocorico.Shared.Helpers
{
    public static class Urls
    {
        public static class ServerAction
        {
            public const string Login = "Login";
            public const string Register = "Register";
            public const string Logout = "Logout";
            public const string AddClaimToUser = "AddClaimToUser";
            public const string RemoveClaimFromUser = "RemoveClaimFromUser";
            public const string SandwichBase = "Sandwich";
        }

        private static class Controllers
        {
            public const string Api = "api";
            public const string Authentication = "/Authentication";
        }

        public static class Server
        {
            public const string Login = Controllers.Api + Controllers.Authentication + "/" + ServerAction.Login;
            public const string Register = Controllers.Api + Controllers.Authentication + "/" + ServerAction.Register;
            public const string Logout = Controllers.Api + Controllers.Authentication + "/" + ServerAction.Logout;
            public const string AddClaimToUser = Controllers.Api + Controllers.Authentication + "/" + ServerAction.AddClaimToUser;
            public const string RemoveClaimFromUser = Controllers.Api + Controllers.Authentication + "/" + ServerAction.RemoveClaimFromUser;
            public const string SandwichBase = Controllers.Api + "/" + ServerAction.SandwichBase;
        }

        public static class Client
        {
            public const string Home = "/";
            public const string Login = "login";
            public const string Register = "register";
            public const string GetAllSandwich = "sandwiches";
            public const string EditSandwich = "sandwiches/edit";
            public const string AddSandwich = "addSandwich";
        }
    }
}
