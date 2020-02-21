﻿using Cocorico.Authentication;
using Cocorico.Client.Application;
using Cocorico.Client.Blazor.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Blazor.DependencyInjection
{
    public static class BlazorInjector
    {
        public static IServiceCollection AddBlazorClient(this IServiceCollection services)
        {
            services.AddAuthorizationCore(options => options.AddCocoricoPolicies());

            services.AddCocoricoClientServices();

            services.AddHttpClients();

            services.AddSignalrClients();

            services.AddViewModels();

            return services;
        }
    }
}
