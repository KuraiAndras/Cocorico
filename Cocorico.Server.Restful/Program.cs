using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace Cocorico.Server.Restful
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();

            static IWebHost BuildWebHost(string[] arguments) =>
                WebHost.CreateDefaultBuilder(arguments)
                    .UseConfiguration(new ConfigurationBuilder()
                        .AddCommandLine(arguments)
                        .Build())
                    .UseStartup<Startup>()
                    .Build();
        }
    }
}
