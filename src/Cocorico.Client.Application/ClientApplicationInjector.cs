using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Application
{
    public static class ClientApplicationInjector
    {
        public static void AddHttpClients(this IServiceCollection services) => HttpClient.HttpClientInjector.AddHttpClients(services);
    }
}
