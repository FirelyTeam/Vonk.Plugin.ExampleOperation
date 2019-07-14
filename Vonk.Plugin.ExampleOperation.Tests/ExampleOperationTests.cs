using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vonk.Core.Context;
using Vonk.Test.Utils;
using Xunit;
using FluentAssertions;
using static Vonk.Plugin.ExampleOperation.Tests.LoggerUtils;
using Microsoft.AspNetCore.Http;
using Hl7.Fhir.ElementModel;
using Vonk.Core.ElementModel;
using System.Linq;

namespace Vonk.Plugin.ExampleOperation.Tests
{
    public class ExampleOperationTests
    {
        private VonkPluginService _testOperationService;
        private ILogger<VonkPluginService> _logger = Logger<VonkPluginService>();
        public ExampleOperationTests()
        {
            _testOperationService = new VonkPluginService(_logger);
        }

        [Fact]
        public async Task TestOperationInstanceLevelShouldSucceed()
        {
            // Create VonkContext for $test (GET / Instance level)
            var testContext = new VonkTestContext(VonkInteraction.instance_custom);
            testContext.Arguments.AddArguments(new[]
            {
                new Argument(ArgumentSource.Path, ArgumentNames.resourceType, "Patient"),
                new Argument(ArgumentSource.Path, ArgumentNames.resourceId, "1234")
            });
            testContext.TestRequest.CustomOperation = "test";
            testContext.TestRequest.Method = "GET";

            // Execute $document
            await _testOperationService.Test(testContext);

            // Check response
            testContext.Response.HttpResult.Should().Be(StatusCodes.Status200OK, "$test should succeed");
            testContext.Response.Payload.GetResourceTypeIndicator().Should().Be("OperationOutcome", "An OperationOutcome should be included in the response");

            testContext.Response.Payload.Children("issue").Count().Should().Be(1, "Only a single issue should be present in the OperationOutcome");
            testContext.Response.Payload.SelectText("issue.severity").Should().Be("information", "The OperationOutcome should informational, not an error");
        }

        [Fact]
        public async Task TestOperationTypeLevelShouldSucceed()
        {
            // Create VonkContext for $test (POST / Type level)
            var testContext = new VonkTestContext(VonkInteraction.instance_custom);
            testContext.Arguments.AddArguments(new[]
            {
                new Argument(ArgumentSource.Path, ArgumentNames.resourceType, "Patient"),
            });
            testContext.TestRequest.CustomOperation = "test";
            testContext.TestRequest.Method = "POST";

            // Execute $document
            await _testOperationService.Test(testContext);

            // Check response
            testContext.Response.HttpResult.Should().Be(StatusCodes.Status200OK, "$test should succeed");
            testContext.Response.Payload.GetResourceTypeIndicator().Should().Be("OperationOutcome", "An OperationOutcome should be included in the response");

            testContext.Response.Payload.Children("issue").Count().Should().Be(1, "Only a single issue should be present in the OperationOutcome");
            testContext.Response.Payload.SelectText("issue.severity").Should().Be("information", "The OperationOutcome should informational, not an error");
        }
    }
}
