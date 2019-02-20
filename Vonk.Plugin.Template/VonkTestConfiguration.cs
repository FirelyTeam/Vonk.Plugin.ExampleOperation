﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vonk.Core.Pluggability;
using Vonk.Core.Support;

namespace Vonk.Plugin.Template
{
    [VonkConfiguration(order: 4600)] // Check for potential conflicts!
    public class VonkPluginConfiguration
    {
        // Add services here to the DI system of ASP.NET Core
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.TryAddScoped<VonkPluginService>(); // Add the service implementation
            services.AddIfNotExists<IConformanceContributor, VonkPluginConformanceContributor>(ServiceLifetime.Transient); // Add operation to Vonk's CapabilityStatement
            return services;
        }

        // Add middleware to the pipeline being built with the builder
        public static IApplicationBuilder Configure(IApplicationBuilder builder)
        {
            // Register interactions (Don't add a "$" sign to the name of the custom operation, it will be added by default)
            builder.OnCustomInteraction(Core.Context.VonkInteraction.instance_custom, "test").AndMethod("GET").HandleAsyncWith<VonkPluginService>((svc, context) => svc.Test(context));
            builder.OnCustomInteraction(Core.Context.VonkInteraction.type_custom, "test").AndMethod("POST").HandleAsyncWith<VonkPluginService>((svc, context) => svc.Test(context));

            return builder;
        }
    }
}
