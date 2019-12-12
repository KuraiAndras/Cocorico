using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Persistence.DependencyInjection
{
    public static class CocoricoDbDependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<CocoricoDbContext>(
                options => options
                    .EnableSensitiveDataLogging()
                    .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
    }
}
