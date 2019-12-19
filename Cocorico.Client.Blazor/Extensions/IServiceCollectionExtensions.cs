using Blazor.Extensions;
using Cocorico.Client.Application.Services.Authentication;
using Cocorico.Client.Application.Services.Basket;
using Cocorico.Client.Application.SignalrClient.WorkerOrders;
using Cocorico.Client.Application.ViewModels.Authentication;
using Cocorico.Client.Application.ViewModels.Ingredient;
using Cocorico.Client.Application.ViewModels.NavMenu;
using Cocorico.Client.Application.ViewModels.Order;
using Cocorico.Client.Application.ViewModels.Sandwich;
using Cocorico.Client.Application.ViewModels.Settings;
using Cocorico.Client.Application.ViewModels.User;
using Cocorico.Client.Blazor.Services.Authentication;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Blazor.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static void AddCocoricoClientServices(this IServiceCollection services)
        {
            services.AddScoped<ICocoricoAuthenticationStateProvider, CocoricoAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => (CocoricoAuthenticationStateProvider)s.GetRequiredService<ICocoricoAuthenticationStateProvider>());

            services.AddSingleton<IBasketService, InMemoryBasketService>();
        }

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
            services.AddTransient<ISettingsViewModel, SettingsViewModel>();
        }

        public static void AddSignalrClients(this IServiceCollection services)
        {
            services.AddTransient<HubConnectionBuilder>();

            services.AddTransient<IWorkerOrdersHubClient, WorkerOrdersHubClient>();
        }
    }
}
