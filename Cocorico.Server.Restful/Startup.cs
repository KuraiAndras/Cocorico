using Cocorico.Server.Domain.Extensions;
using Cocorico.Shared.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Linq;
using System.Net.Http;

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
            services
                .AddMvc()
                .AddNewtonsoftJson();

            services.AddResponseCompression(opts => opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" }));

            services
                .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddSwaggerDocument();

            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = _ => WebHostingEnvironment.IsDevelopment();

                options.Map<HttpRequestException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status503ServiceUnavailable));

                options.Map<EntityNotFoundException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status404NotFound));
                options.Map<UnexpectedException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status500InternalServerError));
                options.Map<InvalidCredentialsException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status403Forbidden));
                options.Map<InvalidCommandException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status400BadRequest));

                options.Map<Exception>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status500InternalServerError));
            });

            services.AddCocoricoServices(Configuration.GetConnectionString("DefaultConnection"));
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

                app.UseSwagger();
                app.UseSwaggerUi3();
            }

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseProblemDetails();

            //MVC
            app.UseRouting();

            app.UseEndpoints(routes =>
            {
                routes.MapDefaultControllerRoute();
            });

            app.UseBlazor<Client.Blazor.Startup>();
        }
    }
}
