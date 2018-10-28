using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SLOBSharp.Client.Requests;
using SLOBSharp.Client.Responses;
using SLOBSharp.Domain.Services;
using SLOBSharp.Tests.TestingResources.Pipes;
using Xunit;

namespace SLOBSharp.Tests.Domain.Services
{
    internal class SlobsPipeServiceSteps
    {
        private ISlobsRequest slobsRequest;
        private string requestId;
        private SlobsRpcResponse mockedSlobsRpcResponse;
        private ISlobsService slobsPipeService;
        private SlobsRpcResponse slobsRpcResponse;
        private List<ISlobsRequest> slobsRequests;

        internal void GivenIHaveASlobsRequest()
        {
            this.slobsRequest = SlobsRequestBuilder.NewRequest()
                                                   .SetRequestId(this.requestId)
                                                   .SetMethod("TestMethod")
                                                   .SetResource("TestResource")
                                                   .AddArgs("test-arg1", "test-arg2")
                                                   .BuildRequest();
        }

        internal void GivenTheTestNamedPipeServerReturnsTheResponseUponRequest(TestNamedPipeServer pipeServer)
        {
            pipeServer.OnRequest(this.slobsRequest).Return(this.mockedSlobsRpcResponse);
        }

        internal void GivenIHaveAMockedSlobsRpcResponse()
        {
            var slobsResult = new SlobsResult { Id = this.requestId };
            this.mockedSlobsRpcResponse = new SlobsRpcResponse { Result = new[] { slobsResult }, Id = this.requestId };
        }

        internal void WhenICallExecuteRequest()
        {
            this.slobsRpcResponse = this.slobsPipeService.ExecuteRequest(this.slobsRequest);
        }

        internal void ThenTheSlobsRpcResponseShouldBeTheMockedResponse()
        {
            Assert.Equal(this.mockedSlobsRpcResponse.Result.First().Id, this.slobsRpcResponse.Result.First().Id);
        }

        internal void ThenIShouldReceiveASlobsRpcResponse()
        {
            Assert.NotNull(this.slobsRpcResponse);

            Assert.NotNull(this.slobsRpcResponse.Id);
            Assert.NotEmpty(this.slobsRpcResponse.Id);

            Assert.NotNull(this.slobsRpcResponse.Result);
            Assert.NotEmpty(this.slobsRpcResponse.Result);

            Assert.NotNull(this.slobsRpcResponse.Result.FirstOrDefault());
        }

        internal async Task WhenICallExecuteRequestAsync()
        {
            this.slobsRpcResponse = await this.slobsPipeService.ExecuteRequestAsync(this.slobsRequest).ConfigureAwait(false);
        }

        internal void GivenIHaveARequestId()
        {
            this.requestId = Guid.NewGuid().ToString("N");
        }

        internal void GivenIHaveASlobsRequestAsList()
        {
            this.GivenIHaveASlobsRequest();
            this.slobsRequests = new List<ISlobsRequest> { this.slobsRequest };
        }

        internal void GivenIHaveASlobsPipeService(string pipeName)
        {
            this.slobsPipeService = new SlobsPipeService(pipeName);
        }

        internal void WhenICallExecuteRequests()
        {
            var slobsRpcResponses = this.slobsPipeService.ExecuteRequests(this.slobsRequests);
            this.slobsRpcResponse = slobsRpcResponses.FirstOrDefault();
        }

        internal async Task WhenICallExecuteRequestsAsync()
        {
            var slobsRpcResponses = await this.slobsPipeService.ExecuteRequestsAsync(this.slobsRequests).ConfigureAwait(false);
            this.slobsRpcResponse = slobsRpcResponses.FirstOrDefault();
        }
    }
}