using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Vonk.Core.Support;

namespace Vonk.Plugin.ExampleOperation
{
    internal class CustomContentTypeMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<VonkPluginService> _logger;

        private const string _exampleContentType = "ExampleContentType";

        public CustomContentTypeMiddleware(RequestDelegate next, ILogger<VonkPluginService> logger)
        {
            Check.NotNull(next, nameof(next));
            Check.NotNull(logger, nameof(logger));
            _next = next;
            _logger = logger;
        }

        // VonkPluginMiddleware can directly access the HttpContext, as opposed to the VonkPluginService
        public async Task Invoke(HttpContext httpContext)
        {
            _logger.LogDebug("CustomContentTypeMiddleware - Begin Invoke");

            // Sent back an HTTP response directly for a special non-FHIR ContentType
            var contentType = httpContext.Request.ContentType;
            if (!(contentType is null) && contentType.Contains(_exampleContentType))
            {
                httpContext.Response.StatusCode = StatusCodes.Status200OK;
                httpContext.Response.ContentType = _exampleContentType;
                await httpContext.Response.WriteAsync($"Success - {_exampleContentType}");
                return;
            }

            await _next(httpContext);
            _logger.LogDebug("CustomContentTypeMiddleware - End Invoke");
        }

    }
}
