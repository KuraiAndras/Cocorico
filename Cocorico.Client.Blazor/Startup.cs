using Blazor.Extensions.Storage;
using Cocorico.Client.Domain.Helpers;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Client.Domain.Services.Basket;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStorage();
            services.AddSingleton<ICocoricoClientAuthenticationService, CocoricoClientAuthenticationService>();
            services.AddSingleton<IBasketService, InMemoryBasketService>();

            services.AddTransient<ISandwichClient, SandwichClient>();
            services.AddTransient<IUserClient, UserClient>();
            services.AddTransient<IOrderClient, OrderClient>();
            services.AddTransient<IAuthenticationClient, AuthenticationClient>();
            services.AddTransient<IIngredientClient, IngredientClient>();
        }

        // ReSharper disable once UnusedMember.Global
        public void Configure(IComponentsApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
