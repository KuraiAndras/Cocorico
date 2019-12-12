using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cocorico.Mappings
{
    public static class DependencyInjection
    {
        public static void AddMappings(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.Load($"{nameof(Cocorico)}.{nameof(Mappings)}"));
    }
}
