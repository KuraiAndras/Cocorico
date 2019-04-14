using Cocorico.Server.Domain.Helpers;
using Cocorico.Server.Domain.Models;
using Cocorico.Server.Domain.Models.Entities.User;
using Cocorico.Server.Domain.Services.Authentication;
using Cocorico.Server.Domain.Services.Sandwich;
using Cocorico.Shared.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;
using Cocorico.Server.Domain.Services.User;

namespace Cocorico.Server.Domain.Extensions
{
    // ReSharper disable once InconsistentNaming
    public static class IServiceCollectionExtension
    {
        public static void AddCocoricoServices(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<CocoricoDbContext>(options => options.UseSqlServer(connectionString));
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
            });

            //Services
            services.AddScoped<IServerCocoricoAuthenticationService, ServerCocoricoAuthenticationService>();
            services.AddScoped<IServerSandwichService, ServerSandwichService>();
            services.AddScoped<IServerUserService, ServerUserService>();
        }
    }
}
