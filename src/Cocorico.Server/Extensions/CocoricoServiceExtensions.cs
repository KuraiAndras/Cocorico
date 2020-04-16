using Cocorico.Application.Orders.Services.Price;
using Cocorico.Application.Orders.Services.RotatingId;
using Cocorico.Application.Users.Services;
using Cocorico.Persistence;
using Cocorico.Persistence.Entities;
using Cocorico.Shared.Authentication;
using Cocorico.Shared.Exceptions;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;

namespace Cocorico.Server.Extensions
{
    public static class CocoricoServiceExtensions
    {
        public static void AddCocoricoIdentityConfiguration(this IServiceCollection services)
        {
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

            services.AddAuthorization(options => options.AddCocoricoPolicies());

            services
                .AddAuthentication(options => options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddScoped<CocoricoDbContext>();
        }

        public static void AddCocoricoServices(this IServiceCollection services)
        {
            services.AddTransient<IOrderRotatingIdService, MemoryOrderRotatingIdService>();
            services.AddTransient<IClaimService, ClaimService>();
            services.AddTransient<IPriceCalculator, PriceCalculator>();
        }

        public static void AddCocoricoProblemDetails(this IServiceCollection services, IWebHostEnvironment webHostingEnvironment) =>
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = (_, __) => webHostingEnvironment.IsDevelopment();

                options.MapToStatusCode<HttpRequestException>(StatusCodes.Status503ServiceUnavailable);

                options.MapToStatusCode<EntityNotFoundException>(StatusCodes.Status404NotFound);
                options.MapToStatusCode<UnexpectedException>(StatusCodes.Status500InternalServerError);
                options.MapToStatusCode<InvalidCredentialsException>(StatusCodes.Status403Forbidden);
                options.MapToStatusCode<InvalidCommandException>(StatusCodes.Status400BadRequest);

                options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
            });
    }
}
