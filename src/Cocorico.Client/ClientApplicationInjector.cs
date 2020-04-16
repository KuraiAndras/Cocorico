using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client
{
    public static class ClientApplicationInjector
    {
        public static void AddHttpClients(this IServiceCollection services) => HttpClient.HttpClientInjector.AddHttpClients(services);
    }
}
