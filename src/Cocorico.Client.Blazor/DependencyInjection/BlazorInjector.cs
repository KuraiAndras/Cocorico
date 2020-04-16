﻿using Cocorico.Client.Blazor.Extensions;
using Cocorico.Shared.Authentication;
using Microsoft.Extensions.DependencyInjection;

namespace Cocorico.Client.Blazor.DependencyInjection
{
    public static class BlazorInjector
    {
        public static IServiceCollection AddBlazorClient(this IServiceCollection services)
        {
            services.AddAuthorizationCore(options => options.AddCocoricoPolicies());

            services.AddCocoricoClientServices();

            services.AddClient();

            services.AddSignalrClients();

            services.AddViewModels();

            return services;
        }
    }
}