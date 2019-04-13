using Cocorico.Server.Api.Extensions;
using Cocorico.Server.Api.Models;
using Cocorico.Server.Api.Models.Entities.User;
using Cocorico.Server.Helpers;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Security.Claims;

namespace Cocorico.Server
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddMvc()
                .AddNewtonsoftJson();

            services.AddResponseCompression(opts => opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "application/octet-stream" }));

            services.AddDbContext<CocoricoDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services
                .AddIdentity<CocoricoUser, IdentityRole>(config => config.User.RequireUniqueEmail = true)
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

            services
                .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.Administrator, policy => policy.RequireClaim(ClaimTypes.Role, Claims.Admin));
                options.AddPolicy(Policies.Customer, policy => policy.RequireClaim(ClaimTypes.Role, Claims.Customer));
                options.AddPolicy(Policies.User, policy => policy.RequireClaim(ClaimTypes.Role, Claims.User));
            });

            services.AddSwaggerGen(c => c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" }));

            services.AddCocoricoServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBlazorDebugging();

                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1"));
            }

            app.UseAuthentication();

            app.UseMvc(routes => routes.MapRoute(name: "default", template: "{controller}/{action}/{id?}"));

            app.UseBlazor<Client.Startup>();
        }
    }
}
