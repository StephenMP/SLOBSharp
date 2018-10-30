using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Moq;
using SLOBSharp.Client;
using SLOBSharp.Client.Requests;
using SLOBSharp.Client.Responses;
using SLOBSharp.Domain.Services;
using Xunit;

namespace SLOBSharp.Tests.Client
{
    internal class SlobsPipeClientSteps
    {
        private ISlobsRequest slobsRequest;
        private SlobsRpcResponse mockedSlobsRpcResponse;
        private Mock<ISlobsService> mockedSlobsPipeService;

        internal void ThenIShouldHaveCreatedANewPipeClient()
        {
            Assert.NotNull(this.slobsPipeClient);
        }

        internal void WhenINewUpAPipeClient(string pipeName = null)
        {
            if (pipeName != null)
            {
                this.slobsPipeClient = new SlobsPipeClient(pipeName);
            }
            else
            {
                this.slobsPipeClient = new SlobsPipeClient();
            }
        }

        private ISlobsClient slobsPipeClient;
        private SlobsRpcResponse slobsRpcResponse;
        private IEnumerable<SlobsRpcResponse> slobsRpcResponses;
        private readonly List<ISlobsRequest> slobsRequests;
        private readonly List<SlobsRpcResponse> mockedSlobsRpcResponses;

        public SlobsPipeClientSteps()
        {
            this.slobsRequests = new List<ISlobsRequest>();
            this.mockedSlobsRpcResponses = new List<SlobsRpcResponse>();
        }

        internal void WhenINewUpAPipeClientUsingStaticCreator(string pipeName = null)
        {
            if (pipeName != null)
            {
                this.slobsPipeClient = SlobsClient.NewPipeClient(pipeName);
            }
            else
            {
                this.slobsPipeClient = SlobsClient.NewPipeClient();
            }
        }

        internal void GivenIHaveARequest()
        {
            var arg = Guid.NewGuid().ToString("N");
            var method = Guid.NewGuid().ToString("N");
            var requestId = Guid.NewGuid().ToString("N");
            var resource = Guid.NewGuid().ToString("N");

            this.slobsRequest = SlobsRequestBuilder.NewRequest().AddArgs(arg).SetMethod(method).SetRequestId(requestId).SetResource(resource).BuildRequest();
        }

        internal void ThenTheResponsesShouldBeTheMockedResponses()
        {
            foreach (var slobsRpcResponse in this.slobsRpcResponses)
            {
                Assert.Contains(this.mockedSlobsRpcResponses, r => r.Id == slobsRpcResponse.Id);
            }
        }

        internal void ThenIShouldReceiveResponses()
        {
            Assert.NotNull(this.slobsRpcResponses);
            Assert.NotEmpty(this.slobsRpcResponses);

            foreach (var slobsRpcResponse in this.slobsRpcResponses)
            {
                Assert.NotNull(slobsRpcResponse);
                Assert.NotNull(slobsRpcResponse.Result);
                Assert.Null(slobsRpcResponse.Error);
            }
        }

        internal void WhenICallExecuteRequests()
        {
            this.slobsRpcResponses = this.slobsPipeClient.ExecuteRequests(this.slobsRequests);
        }

        internal void GivenIHaveMultipleMockedResponse()
        {
            foreach (var slobsRequest in this.slobsRequests)
            {
                var slobsResult = new SlobsResult { Id = slobsRequest.Id };
                var slobsRpcResponse = new SlobsRpcResponse { Result = new[] { slobsResult } };
                this.mockedSlobsRpcResponses.Add(slobsRpcResponse);
            }
        }

        internal async Task WhenICallExecuteRequestsAsync()
        {
            this.slobsRpcResponses = await this.slobsPipeClient.ExecuteRequestsAsync(this.slobsRequests).ConfigureAwait(false);
        }

        internal void GivenIHaveMultipleRequests(int numberOfRequests)
        {
            for (var i = 0; i < numberOfRequests; i++)
            {
                var arg = Guid.NewGuid().ToString("N");
                var method = Guid.NewGuid().ToString("N");
                var requestId = Guid.NewGuid().ToString("N");
                var resource = Guid.NewGuid().ToString("N");
                var slobsRequest = SlobsRequestBuilder.NewRequest().AddArgs(arg).SetMethod(method).SetRequestId(requestId).SetResource(resource).BuildRequest();

                this.slobsRequests.Add(slobsRequest);
            }
        }

        internal void ThenTheResponseShouldBeTheMockedResponse()
        {
            Assert.Equal(this.mockedSlobsRpcResponse.Id, this.slobsRpcResponse.Id);
        }

        internal async Task WhenICallExecuteRequestAsync()
        {
            this.slobsRpcResponse = await this.slobsPipeClient.ExecuteRequestAsync(this.slobsRequest).ConfigureAwait(false);
        }

        internal void ThenIShouldReceiveAResponse()
        {
            Assert.NotNull(this.slobsRpcResponse);
            Assert.NotNull(this.slobsRpcResponse.Result);

            Assert.Null(this.slobsRpcResponse.Error);
        }

        internal void WhenICallExecuteRequest()
        {
            this.slobsRpcResponse = this.slobsPipeClient.ExecuteRequest(this.slobsRequest);
        }

        internal void GivenIHaveASlobsPipeClient()
        {
            this.slobsPipeClient = new SlobsPipeClient(this.mockedSlobsPipeService.Object);
        }

        internal void GivenIHaveAMockedSlobsPipeService()
        {
            this.mockedSlobsPipeService = new Mock<ISlobsService>();
            this.mockedSlobsPipeService.Setup(s => s.ExecuteRequest(this.slobsRequest)).Returns(this.mockedSlobsRpcResponse);
            this.mockedSlobsPipeService.Setup(s => s.ExecuteRequestAsync(this.slobsRequest)).Returns(Task.FromResult(this.mockedSlobsRpcResponse));
            this.mockedSlobsPipeService.Setup(s => s.ExecuteRequests(this.slobsRequests)).Returns(this.mockedSlobsRpcResponses);
            this.mockedSlobsPipeService.Setup(s => s.ExecuteRequestsAsync(this.slobsRequests)).Returns(Task.FromResult(this.mockedSlobsRpcResponses as IEnumerable<SlobsRpcResponse>));
        }

        internal void GivenIHaveAMockedResponse()
        {
            var slobsResult = new SlobsResult { Id = this.slobsRequest.Id };
            this.mockedSlobsRpcResponse = new SlobsRpcResponse { Result = new[] { slobsResult } };
        }
    }
}