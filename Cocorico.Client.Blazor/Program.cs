using Cocorico.Client.Blazor.DependencyInjection;
using Microsoft.AspNetCore.Blazor.Hosting;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor
{
    public class Program
    {
        public static async Task Main(string[] args) => await CreateHostBuilder(args).Build().RunAsync();

        private static WebAssemblyHostBuilder CreateHostBuilder(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);

            builder.RootComponents.Add<App>("app");

            builder.Services.AddBlazorClient();

            return builder;
        }
    }
}
