using Cocorico.Client.Blazor.DependencyInjection;
using Microsoft.AspNetCore.Blazor.Hosting;
using System.Threading.Tasks;

namespace Cocorico.Client.Blazor
{
#pragma warning disable RCS1102 // Make class static.
#pragma warning disable CA1052 // Static holder types should be Static or NotInheritable
#pragma warning disable CC0061 // Asynchronous method can be terminated with the 'Async' keyword.
#pragma warning disable VSD0001 // Asynchronous methods should end with the -Async suffix.
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
#pragma warning restore VSD0001 // Asynchronous methods should end with the -Async suffix.
#pragma warning restore CC0061 // Asynchronous method can be terminated with the 'Async' keyword.
#pragma warning restore RCS1102 // Make class static.
#pragma warning restore CA1052 // Static holder types should be Static or NotInheritable
}
