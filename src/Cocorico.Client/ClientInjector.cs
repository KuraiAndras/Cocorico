using Cocorico.Client.HttpClient;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client
{
    public static class ClientInjector
    {
        public static void AddClient(this IServiceCollection services) => services.AddHttpClients();

        public static void AddHttpClients(this IServiceCollection services)
        {
            services.AddTransient<ISandwichClient, SandwichClient>();
            services.AddTransient<IUserClient, UserClient>();
            services.AddTransient<IOrderClient, OrderClient>();
            services.AddTransient<IAuthenticationClient, AuthenticationClient>();
            services.AddTransient<IIngredientClient, IngredientClient>();
            services.AddTransient<ISettingsClient, SettingsClient>();
        }
    }
}
