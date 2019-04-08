using Cocorico.Server.Services.Authentication;
using Cocorico.Server.Services.Jwt;
using Cocorico.Server.Services.Sandwich;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Server.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtension
    {
        public static void AddCocoricoServices(this IServiceCollection services)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
            services.AddScoped<ICustomAuthenticationService, CustomAuthenticationService>();
            services.AddScoped<ISandwichService, SandwichService>();
        }
    }
}
