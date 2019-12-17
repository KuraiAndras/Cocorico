using Cocorico.Application.Common.Persistence;
using Cocorico.Application.Orders.Services.Price;
using Cocorico.Application.Orders.Services.RotatingId;
using Cocorico.Domain.Entities;
using Cocorico.Domain.Exceptions;
using Cocorico.Domain.Identity;
using Cocorico.Persistence;
using Cocorico.Server.Domain.Services.Authentication;
using Cocorico.Server.Domain.Services.User;
using Hellang.Middleware.ProblemDetails;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Security.Claims;

namespace Cocorico.Server.Restful.Extensions
{
    public static class CocoricoServiceExtensions
    {
        public static void AddCocoricoIdentityConfiguration(this IServiceCollection services)
        {
            // TODO: Move to infrastructure, use Identity server
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

            services.AddScoped<ICocoricoDbContext, CocoricoDbContext>();
        }

        public static void AddCocoricoServices(this IServiceCollection services)
        {
            services.AddTransient<IServerCocoricoAuthenticationService, ServerCocoricoAuthenticationService>();
            services.AddTransient<IServerUserService, ServerUserService>();
            services.AddTransient<IOrderRotatingIdService, MemoryOrderRotatingIdService>();

            services.AddTransient<IPriceCalculator, PriceCalculator>();
        }

        public static void AddCocoricoProblemDetails(this IServiceCollection services, IWebHostEnvironment webHostingEnvironment)
        {
            services.AddProblemDetails(options =>
            {
                options.IncludeExceptionDetails = _ => webHostingEnvironment.IsDevelopment();

                options.Map<HttpRequestException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status503ServiceUnavailable));

                options.Map<EntityNotFoundException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status404NotFound));
                options.Map<UnexpectedException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status500InternalServerError));
                options.Map<InvalidCredentialsException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status403Forbidden));
                options.Map<InvalidCommandException>(exception => new ExceptionProblemDetails(exception, StatusCodes.Status400BadRequest));

                options.Map<Exception>(ex => new ExceptionProblemDetails(ex, StatusCodes.Status500InternalServerError));
            });
        }
    }
}
