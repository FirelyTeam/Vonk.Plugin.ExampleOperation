using Microsoft.Extensions.Options;
using Vonk.Core.Common;
using Vonk.Core.Context.Guards;
using Vonk.Core.Metadata;
using Vonk.Core.Model.Capability;
using Vonk.Core.Pluggability.ContextAware;
using Vonk.Core.Support;

namespace Vonk.Plugin.ExampleOperation
{
    // The ContextAware attribute lets implementations choose for which information models (e.g. FHIR STU3, R4) the service should be made available
    [ContextAware(InformationModels = new[] { VonkConstants.Model.FhirR3, VonkConstants.Model.FhirR4 })]
    internal class VonkPluginCapabilityStatementContributor : ICapabilityStatementContributor
    {
        private const string _operationName = "test";
        private readonly SupportedInteractionOptions _supportedInteractionOptions;

        public VonkPluginCapabilityStatementContributor(IOptions<SupportedInteractionOptions> optionAccessor)
        {
            Check.NotNull(optionAccessor, nameof(optionAccessor));
            _supportedInteractionOptions = optionAccessor.Value;
        }

        // Make the $test operation appear in the CapabilityStatement, if it is declared as supported in the SupportedOperationsOptions 
        // See http://docs.simplifier.net/vonk/configuration/appsettings.html - Enable or disable interactions
        public void ContributeToCapabilityStatement(ICapabilityStatementBuilder builder)
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
