using Cocorico.Client.Blazor.Extensions;
using Cocorico.Shared.Identity;
using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;
using Cocorico.Client.Application;

namespace Cocorico.Client.Blazor
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthorizationCore(options =>
            {
                // TODO: deduplicate
                options.AddPolicy(Policies.Administrator, policy => policy.RequireClaim(ClaimTypes.Role, Claims.Admin));
                options.AddPolicy(Policies.Customer, policy => policy.RequireClaim(ClaimTypes.Role, Claims.Customer));
                options.AddPolicy(Policies.User, policy => policy.RequireClaim(ClaimTypes.Role, Claims.User));
                options.AddPolicy(Policies.Worker, policy => policy.RequireClaim(ClaimTypes.Role, Claims.Worker));
            });

            services.AddCocoricoClientServices();

            services.AddHttpClients();

            services.AddSignalrClients();

            services.AddViewModels();
        }

        public void Configure(IComponentsApplicationBuilder app) => app.AddComponent<App>("app");
    }
}
