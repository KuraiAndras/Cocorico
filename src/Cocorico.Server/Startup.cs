using Cocorico.Application;
using Cocorico.Application.Mappings;
using Cocorico.Persistence.DependencyInjection;
using Cocorico.Server.Extensions;
using Cocorico.Server.Hubs;
using Cocorico.Shared.Helpers;
using Hellang.Middleware.ProblemDetails;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net.Mime;
using System.Reflection;

namespace Cocorico.Server
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        private IWebHostEnvironment WebHostingEnvironment { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            WebHostingEnvironment = environment;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddPersistence(Configuration);

            services.AddCocoricoIdentityConfiguration();

            services.AddApplication();

            services.AddCocoricoServices();

            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddMappings();

            services.AddCocoricoProblemDetails(WebHostingEnvironment);

            services.AddMvc().AddNewtonsoftJson();

            services.AddResponseCompression(opts => opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { MediaTypeNames.Application.Octet, }));

            services.AddSwaggerDocument();
            services.AddSignalR()
                .AddJsonProtocol(options => options.PayloadSerializerOptions.PropertyNamingPolicy = null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        // ReSharper disable once UnusedMember.Global
#pragma warning disable CA1822 // Mark members as static
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();

                app.UseOpenApi();
                app.UseSwaggerUi3();
            }

            app.UseProblemDetails();

            app.UseStaticFiles();
            app.UseClientSideBlazorFiles<Client.Blazor.Program>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<WorkerViewOrderHub>(HubNames.WorkerViewOrderHubNames.Name);
                endpoints.MapFallbackToClientSideBlazor<Client.Blazor.Program>("index.html");
            });
        }
#pragma warning restore CA1822 // Mark members as static
    }
}
