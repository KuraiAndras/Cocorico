using Blazored.LocalStorage;
using Cocorico.Client.Helpers;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddSingleton<AppState>();
        }

        public void Configure(IComponentsApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
