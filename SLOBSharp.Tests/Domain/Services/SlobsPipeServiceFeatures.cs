using System.Threading.Tasks;
using SLOBSharp.Tests.TestingResources.Pipes;
using Xunit;

namespace SLOBSharp.Tests.Domain.Services
{
    public class SlobsPipeServiceFeatures
    {
        private SlobsPipeServiceSteps steps;

        public SlobsPipeServiceFeatures()
        {
            this.steps = new SlobsPipeServiceSteps();
        }

        /*
         SlobsRpcResponse ExecuteRequest(ISlobsRequest request);

        Task<SlobsRpcResponse> ExecuteRequestAsync(ISlobsRequest request);

        IEnumerable<SlobsRpcResponse> ExecuteRequests(IEnumerable<ISlobsRequest> requests);

        IEnumerable<SlobsRpcResponse> ExecuteRequests(params ISlobsRequest[] requests);

        Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(IEnumerable<ISlobsRequest> requests);

        Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(params ISlobsRequest[] requests);
             */

        [Fact]
        public void CanExecuteARequest()
        {
            using (var pipeServer = new TestNamedPipeServer())
            {
                pipeServer.StartServer();

                this.steps.GivenIHaveARequestId();
                this.steps.GivenIHaveASlobsRequest();
                this.steps.GivenIHaveAMockedSlobsRpcResponse();
                this.steps.GivenTheTestNamedPipeServerReturnsTheResponseUponRequest(pipeServer);
                this.steps.GivenIHaveASlobsPipeService(pipeServer.PipeName);

                this.steps.WhenICallExecuteRequest();

                this.steps.ThenIShouldReceiveASlobsRpcResponse();
                this.steps.ThenTheSlobsRpcResponseShouldBeTheMockedResponse();
            }
        }

        [Fact]
        public async Task CanExecuteARequestAsync()
        {
            using (var pipeServer = new TestNamedPipeServer())
            {
                pipeServer.StartServer();

                this.steps.GivenIHaveARequestId();
                this.steps.GivenIHaveASlobsRequest();
                this.steps.GivenIHaveAMockedSlobsRpcResponse();
                this.steps.GivenTheTestNamedPipeServerReturnsTheResponseUponRequest(pipeServer);
                this.steps.GivenIHaveASlobsPipeService(pipeServer.PipeName);

                await this.steps.WhenICallExecuteRequestAsync();

                this.steps.ThenIShouldReceiveASlobsRpcResponse();
                this.steps.ThenTheSlobsRpcResponseShouldBeTheMockedResponse();
            }
        }

        [Fact]
        public void CanExecuteRequests()
        {
            using (var pipeServer = new TestNamedPipeServer())
            {
                pipeServer.StartServer();

                this.steps.GivenIHaveARequestId();
                this.steps.GivenIHaveASlobsRequestAsList();
                this.steps.GivenIHaveAMockedSlobsRpcResponse();
                this.steps.GivenTheTestNamedPipeServerReturnsTheResponseUponRequest(pipeServer);
                this.steps.GivenIHaveASlobsPipeService(pipeServer.PipeName);

                this.steps.WhenICallExecuteRequests();

                this.steps.ThenIShouldReceiveASlobsRpcResponse();
                this.steps.ThenTheSlobsRpcResponseShouldBeTheMockedResponse();
            }
        }

        [Fact]
        public async Task CanExecuteRequestsAsync()
        {
            using (var pipeServer = new TestNamedPipeServer())
            {
                pipeServer.StartServer();

                this.steps.GivenIHaveARequestId();
                this.steps.GivenIHaveASlobsRequestAsList();
                this.steps.GivenIHaveAMockedSlobsRpcResponse();
                this.steps.GivenTheTestNamedPipeServerReturnsTheResponseUponRequest(pipeServer);
                this.steps.GivenIHaveASlobsPipeService(pipeServer.PipeName);

                await this.steps.WhenICallExecuteRequestsAsync();

                this.steps.ThenIShouldReceiveASlobsRpcResponse();
                this.steps.ThenTheSlobsRpcResponseShouldBeTheMockedResponse();
            }
        }
    }
}
