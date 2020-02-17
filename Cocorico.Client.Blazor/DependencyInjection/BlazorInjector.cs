using Cocorico.Client.Application;
using Cocorico.Client.Blazor.Extensions;
using Cocorico.Shared.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace Cocorico.Client.Blazor.DependencyInjection
{
    public static class BlazorInjector
    {
        public static IServiceCollection AddBlazorClient(this IServiceCollection services)
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

            return services;
        }
    }
}
