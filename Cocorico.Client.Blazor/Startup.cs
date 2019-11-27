using Blazor.Extensions;
using Blazor.Extensions.Storage;
using Cocorico.Client.Blazor.Extensions;
using Cocorico.Client.Blazor.Services.Authentication;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Client.Domain.Services.Basket;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddStorage();

            services.AddAuthorizationCore();
            services.AddScoped<ICocoricoAuthenticationStateProvider, CocoricoAuthenticationStateProvider>();
            services.AddScoped<AuthenticationStateProvider>(s => (CocoricoAuthenticationStateProvider)s.GetRequiredService<ICocoricoAuthenticationStateProvider>());

            services.AddSingleton<IBasketService, InMemoryBasketService>();

            services.AddHttpClients();

            services.AddViewModels();

            //services.AddTransient<HubConnectionBuilder>();
        }

        public void Configure(IComponentsApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
