using Blazor.Extensions.Storage;
using Cocorico.Client.Blazor.Extensions;
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

            services.AddHttpClients();

            services.AddViewModels();
        }

        // ReSharper disable once UnusedMember.Global
        public void Configure(IComponentsApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
