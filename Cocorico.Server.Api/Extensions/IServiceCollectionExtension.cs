using Cocorico.Server.Api.Services.Authentication;
using Cocorico.Server.Api.Services.Sandwich;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Server.Api.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtension
    {
        public static void AddCocoricoServices(this IServiceCollection services)
        {
            services.AddScoped<IServerCocoricoAuthenticationService, ServerCocoricoAuthenticationService>();
            services.AddScoped<IServerSandwichService, ServerSandwichService>();
        }
    }
}
