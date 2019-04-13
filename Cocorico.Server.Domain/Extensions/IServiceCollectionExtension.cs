using Cocorico.Server.Domain.Services.Authentication;
using Cocorico.Server.Domain.Services.Sandwich;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Server.Domain.Extensions
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
