using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Mappings
{
    public static class DependencyInjection
    {
        public static void AddMappings(this IServiceCollection services) =>
            services.AddAutoMapper(typeof(DependencyInjection).Assembly);
    }
}
