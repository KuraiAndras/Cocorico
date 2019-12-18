﻿using Cocorico.Mappings;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Cocorico.Application
{
    public static class DependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddMappings();
            services.AddMediatR(Assembly.GetExecutingAssembly());
        }
    }
}