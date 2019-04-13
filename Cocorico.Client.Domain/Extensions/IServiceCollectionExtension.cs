using Blazored.LocalStorage;
using Cocorico.Client.Domain.Services.Authentication;
using Cocorico.Client.Domain.Services.Sandwich;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Domain.Extensions
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
