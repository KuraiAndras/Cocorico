using Cocorico.Shared.Identity;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Cocorico.Authentication
{
    public static class AuthorizationOptionExtensions
    {
        public static AuthorizationOptions AddCocoricoPolicies(this AuthorizationOptions options)
        {
            options.AddPolicy(Policies.Administrator, policy => policy.RequireClaim(ClaimTypes.Role, ApplicationClaims.Admin));
            options.AddPolicy(Policies.Customer, policy => policy.RequireClaim(ClaimTypes.Role, ApplicationClaims.Customer));
            options.AddPolicy(Policies.User, policy => policy.RequireClaim(ClaimTypes.Role, ApplicationClaims.User));
            options.AddPolicy(Policies.Worker, policy => policy.RequireClaim(ClaimTypes.Role, ApplicationClaims.Worker));

            return options;
        }
    }
}
