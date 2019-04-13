using Blazored.LocalStorage;
using Cocorico.Client.Services.Authentication;
using Cocorico.Client.Services.Sandwich;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddSingleton<ICocoricoClientAuthenticationService, CocoricoClientAuthenticationService>();
            services.AddScoped<IClientSandwichService, ClientSandwichService>();
        }

        public void Configure(IComponentsApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
