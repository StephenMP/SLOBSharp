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
        [InlineData(true)]
        [InlineData(false)]
        public void CanExecuteMultipleRequests(bool asEnumerable)
        {
            this.steps.GivenIHaveMultipleRequests(10);
            this.steps.GivenIHaveMultipleMockedResponse();
            this.steps.GivenIHaveAMockedSlobsPipeService();
            this.steps.GivenIHaveASlobsPipeClient();

            this.steps.WhenICallExecuteRequests(asEnumerable);

            this.steps.ThenIShouldReceiveResponses();
            this.steps.ThenTheResponsesShouldBeTheMockedResponses();
        }


        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public async Task CanExecuteMultipleRequestsAsync(bool asEnumerable)
        {
            this.steps.GivenIHaveMultipleRequests(10);
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
