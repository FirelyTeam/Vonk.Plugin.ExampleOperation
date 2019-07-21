using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vonk.Core.Context;
using Vonk.Core.Pluggability;
using Vonk.Core.Support;

namespace Vonk.Plugin.ExampleOperation
{
    [VonkConfiguration(order: 4600)] // Check for potential conflicts!
    public class VonkExampleServiceConfiguration
    {
        /* Add services here to the DI system of ASP.NET Core
           Make sure to register services that you depend upon as well. 
           All service registration methods in Vonk are idempotent, so they can be safely called multiple times.
        */
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
            builder.OnCustomInteraction(VonkInteraction.instance_custom, "test").AndMethod("GET").HandleAsyncWith<VonkPluginService>((svc, context) => svc.Test(context));
            builder.OnCustomInteraction(VonkInteraction.type_custom, "test").AndMethod("POST").HandleAsyncWith<VonkPluginService>((svc, context) => svc.Test(context));
            return builder;
        }
    }

    [VonkConfiguration(order: 4590)]
    public class VonkExampleMiddlewareConfiguration
    {
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            // VonkPluginMiddleware doesn't have any dependencies on other services, otherwise they should be registered here
            return services;
        }

        public static IApplicationBuilder Configure(IApplicationBuilder builder)
        {
            builder.UseMiddleware<VonkPluginMiddleware>();
            return builder;
        }
    }
}
