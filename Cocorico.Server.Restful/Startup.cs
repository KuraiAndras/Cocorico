using Cocorico.Mappings;
using Cocorico.Persistence.DependencyInjection;
using Cocorico.Server.Restful.Extensions;
using Cocorico.Server.Restful.Hubs;
using Cocorico.Shared.Helpers;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Linq;
using System.Net.Mime;
using Cocorico.Application;

namespace Cocorico.Server.Restful
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

            app.UseClientSideBlazorFiles<Client.Blazor.Startup>();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
                endpoints.MapHub<WorkerViewOrderHub>(HubNames.WorkerViewOrderHubNames.Name);
                endpoints.MapFallbackToClientSideBlazor<Client.Blazor.Startup>("index.html");
            });
        }
    }
}
