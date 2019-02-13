using System;
using Microsoft.Extensions.Options;
using Vonk.Core.Context;
using Vonk.Core.Pluggability;
using Vonk.Core.Support;

namespace Vonk.Plugin.Template
{
    class VonkTestConformanceContributor : IConformanceContributor
    {
        private const string _operationName = "test";
        private readonly SupportedInteractionOptions _supportedInteractionOptions;

        public VonkTestConformanceContributor(IOptions<SupportedInteractionOptions> optionAccessor)
        {
            Check.NotNull(optionAccessor, nameof(optionAccessor));
            _supportedInteractionOptions = optionAccessor.Value;
        }

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
