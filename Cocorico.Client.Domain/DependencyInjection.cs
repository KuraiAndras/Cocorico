using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Domain
{
    public static class DependencyInjection
    {
        public static void AddHttpClients(this IServiceCollection services) => HttpClient.DependencyInjection.AddHttpClients(services);
    }
}
