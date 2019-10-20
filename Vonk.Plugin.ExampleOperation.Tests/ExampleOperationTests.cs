using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Vonk.Core.Context;
using Xunit;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using System.Linq;
using static Vonk.Core.Context.VonkOutcome;
using Vonk.UnitTests.Framework.Helpers;
using static Vonk.UnitTests.Framework.Helpers.LoggerUtils;

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
            testContext.Response.Outcome.Issues.Count().Should().Be(1, "An OperationOutcome should be included in the response");
            testContext.Response.Outcome.IssuesWithSeverity(IssueSeverity.Information).Count().Should().Be(1, "The OperationOutcome should be informational, not an error");
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
            testContext.Response.Outcome.Issues.Count().Should().Be(1, "An OperationOutcome should be included in the response");
            testContext.Response.Outcome.IssuesWithSeverity(IssueSeverity.Information).Count().Should().Be(1, "The OperationOutcome should be informational, not an error");
        }
    }
}
