﻿using AutoMapper;
using Cocorico.Client.Blazor.Extensions;
using Cocorico.Shared.Authentication;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Cocorico.Client.Blazor.DependencyInjection
{
    public static class BlazorInjector
    {
        public static IServiceCollection AddBlazorClient(
            this IServiceCollection services,
            string baseAddress)
        {
            services.AddAuthorizationCore(options => options.AddCocoricoPolicies());

            services.AddCocoricoClientServices();

            services.AddClient();

            services.AddSignalrClients();

            services.AddViewModels();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddSingleton(new System.Net.Http.HttpClient { BaseAddress = new Uri(baseAddress) });

            return services;
        }
    }
}
