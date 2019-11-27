using Blazor.Extensions;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.SignalrClient.WorkerOrders;
using Cocorico.Client.Domain.ViewModels.Authentication;
using Cocorico.Client.Domain.ViewModels.Ingredient;
using Cocorico.Client.Domain.ViewModels.NavMenu;
using Cocorico.Client.Domain.ViewModels.Order;
using Cocorico.Client.Domain.ViewModels.Sandwich;
using Cocorico.Client.Domain.ViewModels.User;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Blazor.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddViewModels(this IServiceCollection services)
        {
            services.AddTransient<ILoginViewModel, LoginViewModel>();
            services.AddTransient<IRegisterViewModel, RegisterViewModel>();
            services.AddTransient<IAddIngredientViewModel, AddIngredientViewModel>();
            services.AddTransient<IEditIngredientViewModel, EditIngredientViewModel>();
            services.AddTransient<IIngredientsViewModel, IngredientsViewModel>();
            services.AddTransient<IAddCustomerOrderViewModel, AddCustomerOrderViewModel>();
            services.AddTransient<IOrdersViewModel, OrdersViewModel>();
            services.AddTransient<IAddSandwichViewModel, AddSandwichViewModel>();
            services.AddTransient<IEditSandwichViewModel, EditSandwichViewModel>();
            services.AddTransient<ISandwichesViewModel, SandwichesViewModel>();
            services.AddTransient<IEditUserClaimViewModel, EditUserClaimViewModel>();
            services.AddTransient<IUsersViewModel, UsersViewModel>();
            services.AddTransient<INavMenuViewModel, NavMenuViewModel>();
        }

        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddTransient<ISandwichClient, SandwichClient>();
            services.AddTransient<IUserClient, UserClient>();
            services.AddTransient<IOrderClient, OrderClient>();
            services.AddTransient<IAuthenticationClient, AuthenticationClient>();
            services.AddTransient<IIngredientClient, IngredientClient>();
        }

        public static void AddSignalrClients(this IServiceCollection services)
        {
            services.AddTransient<HubConnectionBuilder>();

            services.AddTransient<IWorkerOrdersHubClient, WorkerOrdersHubClient>();
        }
    }
}
