using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cocorico.Mappings
{
    public static class MappingsInjector
    {
        public static void AddMappings(this IServiceCollection services) =>
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
    }
}
