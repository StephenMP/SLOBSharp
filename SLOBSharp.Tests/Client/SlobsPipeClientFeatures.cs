using System.Threading.Tasks;
using Xunit;

namespace SLOBSharp.Tests.Client
{
    public class SlobsPipeClientFeatures
    {
        private readonly SlobsPipeClientSteps steps;

        public SlobsPipeClientFeatures()
        {
            this.steps = new SlobsPipeClientSteps();
        }

        [Fact]
        public void CanCreateWithoutAPipeName()
        {
            this.steps.WhenINewUpAPipeClient();

            this.steps.ThenIShouldHaveCreatedANewPipeClient();
        }

        [Fact]
        public void CanCreateWithAPipeName()
        {
            this.steps.WhenINewUpAPipeClient("slobs");

            this.steps.ThenIShouldHaveCreatedANewPipeClient();
        }

        [Fact]
        public void CanCreateWithoutAPipeNameUsingStaticCreator()
        {
            this.steps.WhenINewUpAPipeClientUsingStaticCreator();

            this.steps.ThenIShouldHaveCreatedANewPipeClient();
        }

        [Fact]
        public void CanCreateWithAPipeNameUsingStaticCreator()
        {
            this.steps.WhenINewUpAPipeClientUsingStaticCreator("slobs");

            this.steps.ThenIShouldHaveCreatedANewPipeClient();
        }

        [Fact]
        public void CanExecuteARequest()
        {
            this.steps.GivenIHaveARequest();
            this.steps.GivenIHaveAMockedResponse();
            this.steps.GivenIHaveAMockedSlobsPipeService();
            this.steps.GivenIHaveASlobsPipeClient();

            this.steps.WhenICallExecuteRequest();

            this.steps.ThenIShouldReceiveAResponse();
            this.steps.ThenTheResponseShouldBeTheMockedResponse();
        }

        [Theory]
        [InlineData(true, 1)]
        [InlineData(true, 2)]
        [InlineData(true, 3)]
        [InlineData(true, 4)]
        [InlineData(true, 5)]
        [InlineData(true, 6)]
        [InlineData(true, 7)]
        [InlineData(true, 8)]
        [InlineData(true, 9)]
        [InlineData(true, 10)]
        [InlineData(false, 1)]
        [InlineData(false, 2)]
        [InlineData(false, 3)]
        [InlineData(false, 4)]
        [InlineData(false, 5)]
        [InlineData(false, 6)]
        [InlineData(false, 7)]
        [InlineData(false, 8)]
        [InlineData(false, 9)]
        [InlineData(false, 10)]
        public void CanExecuteMultipleRequests(bool asEnumerable, int numberOfRequests)
        {
            this.steps.GivenIHaveMultipleRequests(numberOfRequests);
            this.steps.GivenIHaveMultipleMockedResponse();
            this.steps.GivenIHaveAMockedSlobsPipeService();
            this.steps.GivenIHaveASlobsPipeClient();

            this.steps.WhenICallExecuteRequests(asEnumerable);

            this.steps.ThenIShouldReceiveResponses();
            this.steps.ThenTheResponsesShouldBeTheMockedResponses();
        }

        [Theory]
        [InlineData(true, 1)]
        [InlineData(true, 2)]
        [InlineData(true, 3)]
        [InlineData(true, 4)]
        [InlineData(true, 5)]
        [InlineData(true, 6)]
        [InlineData(true, 7)]
        [InlineData(true, 8)]
        [InlineData(true, 9)]
        [InlineData(true, 10)]
        [InlineData(false, 1)]
        [InlineData(false, 2)]
        [InlineData(false, 3)]
        [InlineData(false, 4)]
        [InlineData(false, 5)]
        [InlineData(false, 6)]
        [InlineData(false, 7)]
        [InlineData(false, 8)]
        [InlineData(false, 9)]
        [InlineData(false, 10)]
        public async Task CanExecuteMultipleRequestsAsync(bool asEnumerable, int numberOfRequests)
        {
            this.steps.GivenIHaveMultipleRequests(numberOfRequests);
            this.steps.GivenIHaveMultipleMockedResponse();
            this.steps.GivenIHaveAMockedSlobsPipeService();
            this.steps.GivenIHaveASlobsPipeClient();

            await this.steps.WhenICallExecuteRequestsAsync(asEnumerable).ConfigureAwait(false);

            this.steps.ThenIShouldReceiveResponses();
            this.steps.ThenTheResponsesShouldBeTheMockedResponses();
        }

        [Fact]
        public async Task CanExecateARequestAsync()
        {
            this.steps.GivenIHaveARequest();
            this.steps.GivenIHaveAMockedResponse();
            this.steps.GivenIHaveAMockedSlobsPipeService();
            this.steps.GivenIHaveASlobsPipeClient();

            await this.steps.WhenICallExecuteRequestAsync().ConfigureAwait(false);

            this.steps.ThenIShouldReceiveAResponse();
            this.steps.ThenTheResponseShouldBeTheMockedResponse();
        }
    }
}
