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
using System.Net.Mime;
using System.Security.Claims;
using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Models.Entities;
using Cocorico.Server.Domain.Services.Authentication;
using Cocorico.Server.Domain.Services.Order;
using Cocorico.Server.Domain.Services.Sandwich;
using Cocorico.Server.Domain.Services.User;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Components.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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
            services.AddDbContext<CocoricoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services
                .AddIdentity<CocoricoUser, IdentityRole>(identityOptions => identityOptions.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<CocoricoDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 5;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = true;
            });

            //Instant logout
            services.Configure<SecurityStampValidatorOptions>(options => options.ValidationInterval = TimeSpan.Zero);

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Administrator, policy => policy.RequireClaim(ClaimTypes.Role, Claims.Admin));
                options.AddPolicy(Policies.Customer, policy => policy.RequireClaim(ClaimTypes.Role, Claims.Customer));
                options.AddPolicy(Policies.User, policy => policy.RequireClaim(ClaimTypes.Role, Claims.User));
                options.AddPolicy(Policies.Worker, policy => policy.RequireClaim(ClaimTypes.Role, Claims.Worker));
            });

            services
                .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            //Services
            services.AddScoped<IServerCocoricoAuthenticationService, ServerCocoricoAuthenticationService>();
            services.AddScoped<IServerSandwichService, ServerSandwichService>();
            services.AddScoped<IServerUserService, ServerUserService>();
            services.AddScoped<IServerOrderService, ServerOrderService>();

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

            services
                .AddMvc()
                .AddNewtonsoftJson();

            services.AddResponseCompression(opts => opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[]
            {
                MediaTypeNames.Application.Octet,
                WasmMediaTypeNames.Application.Wasm,
            }));

            services.AddSwaggerDocument();
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

            app.UseProblemDetails();

            //MVC
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(routes =>
            {
                routes.MapDefaultControllerRoute();
            });

            app.UseBlazor<Client.Blazor.Startup>();
        }
    }
}
