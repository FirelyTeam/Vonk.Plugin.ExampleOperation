using System;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Vonk.Core.Context;
using Vonk.Core.Context.Features;
using Vonk.Core.Pluggability;
using Vonk.Core.Support;

namespace Vonk.Plugin.Template
{
    internal class VonkPluginService

    {
        private readonly ILogger<VonkPluginService> _logger;

        // You can have any dependency (e.g. ISearchRepository, IResourceChangeRepository) injected here, if they have been registered before
        public VonkPluginService(ILogger<VonkPluginService> logger)
        {
            Check.NotNull(logger, nameof(logger));
            _logger = logger;
        }

        /// <summary>
        /// Handle GET [base]/<Resource>/id/$test
        /// </summary>
        /// <param name="vonkContext">IVonkContext for details of the request and providing the response</param>
        /// <returns></returns>
        [InteractionHandler(VonkInteraction.instance_custom, CustomOperation = "test", Method = "GET")]
        public async Task TestInstanceGET(IVonkContext vonkContext)
        {
            var (_, _, response) = vonkContext.Parts();
            vonkContext.Arguments.Handled();
            response.HttpResult = 200;

            _logger.LogInformation("Executed $test");
            // Insert complex operation
        }

        [InteractionHandler(VonkInteraction.type_custom, CustomOperation = "test", Method = "POST")]
        public async Task TestTypePOST(IVonkContext vonkContext)
        {
            var (_, _, response) = vonkContext.Parts();
            vonkContext.Arguments.Handled();
            response.HttpResult = 200;

            _logger.LogInformation("Executed $test");
            // Insert complex operation
        }

    }
}
