using Blazored.LocalStorage;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Client.Domain.Services.Basket;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Domain.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtension
    {
        public static void AddCocoricoClientServices(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddSingleton<ICocoricoClientAuthenticationService, CocoricoClientAuthenticationService>();
            services.AddSingleton<IBasketService, InMemoryBasketService>();

            services.AddScoped<ISandwichClient, SandwichClient>();
            services.AddScoped<IUserClient, UserClient>();
            services.AddScoped<IOrderClient, OrderClient>();
            services.AddScoped<IAuthenticationClient, AuthenticationClient>();
        }
    }
}
