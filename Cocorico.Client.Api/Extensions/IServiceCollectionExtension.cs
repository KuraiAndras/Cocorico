using Blazored.LocalStorage;
using Cocorico.Client.Api.Services.Authentication;
using Cocorico.Client.Api.Services.Sandwich;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Api.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtension
    {
        public static void AddCocoricoClientServices(this IServiceCollection services)
        {
            services.AddBlazoredLocalStorage();
            services.AddSingleton<ICocoricoClientAuthenticationService, CocoricoClientAuthenticationService>();
            services.AddScoped<IClientSandwichService, ClientSandwichService>();
        }
    }
}
