using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.HttpClient
{
    public static class HttpClientInjector
    {
        public static void AddHttpClients(IServiceCollection services)
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
