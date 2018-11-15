using System;
using System.Threading.Tasks;
using Xunit;

namespace SLOBSharp.Tests.Domain.Services
{
    public class SlobsPipeServiceFeatures : IDisposable
    {
        private readonly SlobsPipeServiceSteps steps;

        public SlobsPipeServiceFeatures()
        {
            this.steps = new SlobsPipeServiceSteps();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public void CanExecuteRequests(int numberOfRequests)
        {
            this.steps.GivenIHaveSlobsRequests(numberOfRequests);
            this.steps.GivenIHaveMultipleMockedResponse();
            this.steps.GivenIHaveAMockedStreamWriter();
            this.steps.GivenIHaveAMockedStreamReader();
            this.steps.GivenIHaveASlobsPipeService();

            this.steps.WhenICallExecuteRequests();

            this.steps.ThenIShouldReceiveAResponse();
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        [InlineData(6)]
        [InlineData(7)]
        [InlineData(8)]
        [InlineData(9)]
        [InlineData(10)]
        public async Task CanExecuteRequestsAsync(int numberOfRequests)
        {
            this.steps.GivenIHaveSlobsRequests(numberOfRequests);
            this.steps.GivenIHaveMultipleMockedResponse();
            this.steps.GivenIHaveAMockedStreamWriter();
            this.steps.GivenIHaveAMockedStreamReader();
            this.steps.GivenIHaveASlobsPipeService();

            await this.steps.WhenICallExecuteRequestsAsync().ConfigureAwait(false);

            this.steps.ThenIShouldReceiveAResponse();
        }

        #region IDisposable Support
        private bool disposedValue;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.steps?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }
        #endregion
    }
}
