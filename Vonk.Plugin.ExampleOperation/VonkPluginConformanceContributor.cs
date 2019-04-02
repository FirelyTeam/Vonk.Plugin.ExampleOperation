using System;
using Microsoft.Extensions.Options;
using Vonk.Core.Context;
using Vonk.Core.Pluggability;
using Vonk.Core.Support;

namespace Vonk.Plugin.ExampleOperation
{
    internal class VonkPluginConformanceContributor : IConformanceContributor
    {
        private const string _operationName = "test";
        private readonly SupportedInteractionOptions _supportedInteractionOptions;

        public VonkPluginConformanceContributor(IOptions<SupportedInteractionOptions> optionAccessor)
        {
            Check.NotNull(optionAccessor, nameof(optionAccessor));
            _supportedInteractionOptions = optionAccessor.Value;
        }

        // Make the $test operation appear in the CapabilityStatement, if it is declared as supported in the SupportedOperationsOptions 
        // See http://docs.simplifier.net/vonk/configuration/appsettings.html - Enable or disable interactions
        public void Conformance(IConformanceBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));
            if (_supportedInteractionOptions.SupportsCustomOperation(_operationName))
            {
                builder.UseRestComponentEditor(rce =>
                {
                    rce.AddOperation(_operationName, "http://example.com/fhir/OperationDefinition/test-operation");
                });
            }
        }
    }
}
