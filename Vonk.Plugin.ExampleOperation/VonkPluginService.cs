using System;
using Microsoft.Extensions.Logging;
using Vonk.Core.Context;
using Vonk.Core.Support;
using Task = System.Threading.Tasks.Task;
using static Vonk.Core.Context.VonkOutcome;

namespace Vonk.Plugin.ExampleOperation
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
        /// Handle GET [base]/<Resource>/id/$test and POST [base]/<Resource>/$test
        /// </summary>
        /// <param name="vonkContext">IVonkContext for details of the request and providing the response</param>
        /// <returns></returns>
        public async Task Test(IVonkContext vonkContext)
        {
            var (_, _, response) = vonkContext.Parts();
            vonkContext.Arguments.Handled();
            response.HttpResult = 200;

            _ = await Task.FromResult(true); // Replace with own complex operation

            vonkContext.Response.Outcome.AddIssue(IssueSeverity.Information, IssueType.Informational, diagnostics: "$test operation was executed successfully");

            _logger.LogDebug("VonkPluginService - Executed $test"); // Adjust log level in logsettings.instance.json to see the message
        }

        public async Task PrepareTest(IVonkContext vonkContext)
        {
            _logger.LogDebug("VonkPluginService - About to execute $test");
            _ = await Task.FromResult(true);
        }

        public async Task PostHandlerTest(IVonkContext vonkContext)
        {
            _logger.LogDebug("VonkPluginService - PostHandler $test");
            _ = await Task.FromResult(true);
        }
    }
}
