using Cocorico.Server.Services.Jwt;
using Cocorico.Server.Services.Sandwich;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Server.Extensions
{
    public static class IServiceCollectionExtension
    {
        public static void AddCocoricoServices(this IServiceCollection services)
        {
            services.AddTransient<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ISandwichService, SandwichService>();
        }
    }
}
