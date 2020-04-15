using Cocorico.Client.Blazor.DependencyInjection;
using Microsoft.AspNetCore.Blazor.Hosting;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor
{
#pragma warning disable RCS1102 // Make class static.
    public class Program
    {
        public static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).Build().RunAsync();

            static WebAssemblyHostBuilder CreateHostBuilder(string[] args)
            {
                var builder = WebAssemblyHostBuilder.CreateDefault(args);

                builder.RootComponents.Add<App>("app");

                builder.Services.AddBlazorClient();

                return builder;
            }
        }
    }
#pragma warning restore RCS1102 // Make class static.
}
