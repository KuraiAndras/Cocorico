using Blazored.LocalStorage;
using Cocorico.Client.Blazor.Extensions;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();

            services.AddAuthorizationCore();

            services.AddCocoricoClientServices();

            services.AddHttpClients();

            services.AddSignalrClients();

            services.AddViewModels();
        }

        public void Configure(IComponentsApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
