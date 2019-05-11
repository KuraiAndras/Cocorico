namespace Cocorico.Shared.Helpers
{
    public static class Urls
    {
        public static class ServerAction
        {
            public const string Login = nameof(Login);
            public const string Register = nameof(Register);
            public const string Logout = nameof(Logout);
            public const string AddClaimToUser = nameof(AddClaimToUser);
            public const string RemoveClaimFromUser = nameof(RemoveClaimFromUser);
            public const string GetAllUsersForAdminPage = nameof(GetAllUsersForAdminPage);
            public const string GetUserForAdminPage = nameof(GetUserForAdminPage);
            public const string PendingOrdersForWorker = nameof(PendingOrdersForWorker);
            public const string GetAllOrderForCustomer = nameof(GetAllOrderForCustomer);
            public const string UpdateOrder = nameof(UpdateOrder);
        }

        private static class Controllers
        {
            public const string Api = "api";
            public const string Authentication = "/Authentication";
            public const string User = "/User";
            public const string Order = "/Order";
            public const string Sandwich = "/Sandwich";
        }

        public static class Server
        {
            public const string Login = Controllers.Api + Controllers.Authentication + "/" + ServerAction.Login;
            public const string Register = Controllers.Api + Controllers.Authentication + "/" + ServerAction.Register;
            public const string Logout = Controllers.Api + Controllers.Authentication + "/" + ServerAction.Logout;
            public const string AddClaimToUser = Controllers.Api + Controllers.Authentication + "/" + ServerAction.AddClaimToUser;
            public const string RemoveClaimFromUser = Controllers.Api + Controllers.Authentication + "/" + ServerAction.RemoveClaimFromUser;
            public const string SandwichBase = Controllers.Api + Controllers.Sandwich;
            public const string GetAllUsersForAdminPage = Controllers.Api + Controllers.User + "/" + ServerAction.GetAllUsersForAdminPage;
            public const string GetUserForAdminPage = Controllers.Api + Controllers.User + "/" + ServerAction.GetUserForAdminPage;
            public const string OrderBase = Controllers.Api + Controllers.Order;
            public const string PendingOrdersForWorker = Controllers.Api + Controllers.Order + "/" + ServerAction.PendingOrdersForWorker;
            public const string GetAllOrderForCustomer = Controllers.Api + Controllers.Order + "/" + ServerAction.GetAllOrderForCustomer;
            public const string UpdateOrder = Controllers.Api + Controllers.Order + "/" + ServerAction.UpdateOrder;
        }

        public static class Client
        {
            public const string Home = "/";
            public const string Login = "login";
            public const string Register = "register";
            public const string GetAllSandwich = "sandwiches";
            public const string EditSandwich = "sandwiches/edit";
            public const string AddSandwich = "addSandwich";
            public const string Users = "users";
            public const string AdminEditUserClaim = "adminEditUserClaim";
            public const string AddCustomerOrder = nameof(AddCustomerOrder);
            public const string OrdersToDeliver = nameof(OrdersToDeliver);
            public const string OrdersToMake = nameof(OrdersToMake);
        }
    }
}
