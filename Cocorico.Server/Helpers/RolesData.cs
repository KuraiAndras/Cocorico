using System;
using System.Linq;
using System.Threading.Tasks;
using Cocorico.Server.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Server.Helpers
{
    public static class RolesData
    {
        private static readonly string[] Roles = new[] { "CocoricoUser" };

        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetService<CocoricoDbContext>();

                var pendingMigrations = await dbContext.Database.GetPendingMigrationsAsync();

                if (pendingMigrations.Any())
                {
                    await dbContext.Database.MigrateAsync();

                    var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                    foreach (var role in Roles)
                    {
                        if (!await roleManager.RoleExistsAsync(role))
                        {
                            await roleManager.CreateAsync(new IdentityRole(role));
                        }
                    }
                }
            }
        }
    }
}
