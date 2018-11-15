using Xunit;

namespace SLOBSharp.Tests.Client.Requests
{
    public class SlobsRequestBuilderFeatures
    {
        private readonly SlobsRequestBuilderSteps steps;

        public SlobsRequestBuilderFeatures()
        {
            this.steps = new SlobsRequestBuilderSteps();
        }

        [Theory]
        [InlineData("1", "method1", "resource1", "arg1")]
        [InlineData("2", "method2", "resource2", "arg2")]
        [InlineData("3", "method3", "resource3", "arg3")]
        [InlineData("4", "method4", "resource4", "arg4")]
        [InlineData("5", "method5", "resource5", "arg5")]
        public void CanBuildARequest(string id, string methodName, string resource, string arg)
        {
            this.steps.GivenIHaveAMockedRequest(id, methodName, resource, arg);
            this.steps.GivenIHaveASlobsRequestBuilder();

            this.steps.WhenISetTheRequestIdTo(id);
            this.steps.WhenISetTheMethodTo(methodName);
            this.steps.WhenISetTheResourceTo(resource);
            this.steps.WhenIAddTheArgs(arg);
            this.steps.WhenIBuildTheRequest();

            this.steps.ThenIShouldHaveARequest();
            this.steps.ThenTheRequestShouldEqualTheMockedRequest();
        }

        [Theory]
        [InlineData("1", "method1", "resource1", "arg1", 1)]
        [InlineData("2", "method2", "resource2", "arg2", 1)]
        [InlineData("3", "method3", "resource3", "arg3", 1)]
        [InlineData("4", "method4", "resource4", "arg4", 1)]
        [InlineData("5", "method5", "resource5", "arg5", 1)]
        [InlineData("1", "method1", "resource1", "arg1", 2)]
        [InlineData("2", "method2", "resource2", "arg2", 2)]
        [InlineData("3", "method3", "resource3", "arg3", 2)]
        [InlineData("4", "method4", "resource4", "arg4", 2)]
        [InlineData("5", "method5", "resource5", "arg5", 2)]
        [InlineData("1", "method1", "resource1", "arg1", 3)]
        [InlineData("2", "method2", "resource2", "arg2", 3)]
        [InlineData("3", "method3", "resource3", "arg3", 3)]
        [InlineData("4", "method4", "resource4", "arg4", 3)]
        [InlineData("5", "method5", "resource5", "arg5", 3)]
        [InlineData("1", "method1", "resource1", "arg1", 4)]
        [InlineData("2", "method2", "resource2", "arg2", 4)]
        [InlineData("3", "method3", "resource3", "arg3", 4)]
        [InlineData("4", "method4", "resource4", "arg4", 4)]
        [InlineData("5", "method5", "resource5", "arg5", 4)]
        [InlineData("1", "method1", "resource1", "arg1", 5)]
        [InlineData("2", "method2", "resource2", "arg2", 5)]
        [InlineData("3", "method3", "resource3", "arg3", 5)]
        [InlineData("4", "method4", "resource4", "arg4", 5)]
        [InlineData("5", "method5", "resource5", "arg5", 5)]
        public void CanBuildMultipleRequests(string id, string methodName, string resource, string arg, int numberOfRequests)
        {
            this.steps.GivenIHaveMultipleMockedRequest(id, methodName, resource, numberOfRequests, arg);
            this.steps.GivenIHaveASlobsMultipleRequestBuilder();

            for (var i = 0; i < numberOfRequests; i++)
            {
                this.steps.WhenISetMultipleRequestIdsTo(id);
                this.steps.WhenISetMultipleMethodsTo(methodName);
                this.steps.WhenISetMultipleResourcesTo(resource);
                this.steps.WhenIAddMultipleArgs(arg);

                if (i != numberOfRequests - 1)
                {
                    this.steps.WhenIBeginTheNextRequest();
                }
            }

            this.steps.WhenIBuildTheRequests();

            this.steps.ThenIShouldHaveRequests();
            this.steps.ThenTheRequestsShouldEqualTheMockedRequests();
        }
    }
}
