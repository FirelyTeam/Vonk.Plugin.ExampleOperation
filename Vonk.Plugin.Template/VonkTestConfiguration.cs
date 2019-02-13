using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Vonk.Core.Pluggability;
using Vonk.Core.Support;

namespace Vonk.Plugin.Template
{
    [VonkConfiguration(order: 4600)]
    public class VonkTestConfiguration
    {
        // Add services here to the DI system of ASP.NET Core
        public static IServiceCollection ConfigureServices(IServiceCollection services)
        {
            services.TryAddScoped<VonkTestService>(); // Add the service implementation
            services.AddIfNotExists<IConformanceContributor, VonkTestConformanceContributor>(ServiceLifetime.Transient);
            services.TryAddTransient<VonkTestConformanceContributor>(); // Add operation to Vonk's CapabilityStatement
            return services;
        }

        // Add middleware to the pipeline being built with the builder
        public static IApplicationBuilder Configure(IApplicationBuilder builder)
        {
            // Register interactions
            builder.UseVonkInteractionAsync<VonkTestService>((svc, context) => svc.TestInstanceGET(context), OperationType.Handler);
            builder.UseVonkInteractionAsync<VonkTestService>((svc, context) => svc.TestTypePOST(context), OperationType.Handler);

            return builder;
        }
    }
}
