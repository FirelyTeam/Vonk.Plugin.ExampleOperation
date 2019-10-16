using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Vonk.Core.Context.Features;
using Vonk.Core.Support;

namespace Vonk.Plugin.ExampleOperation
{
    internal class VonkPluginMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<VonkPluginService> _logger;

        public VonkPluginMiddleware(RequestDelegate next, ILogger<VonkPluginService> logger)
        {
            Check.NotNull(next, nameof(next));
            Check.NotNull(logger, nameof(logger));
            _next = next;
            _logger = logger;
        }

        // VonkPluginMiddleware can directly access the HttpContext, as opposed to the VonkPluginService
        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogDebug("VonkPluginMiddleware - Begin Invoke");

            var (request, args, response) = httpContext.Vonk().Parts(); // Turn HttpContext into VonkContext
            httpContext.Items["key"] = "value";                         // A middleware can inject request specific items into the HttpContext

            await _next(httpContext);                                   // Call next in case the middleware does not handle the request directly!
            _logger.LogDebug("VonkPluginMiddleware - End Invoke");
        }

    }
}
