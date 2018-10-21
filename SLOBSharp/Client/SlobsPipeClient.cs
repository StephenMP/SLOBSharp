using System.Collections.Generic;
using System.Threading.Tasks;
using SLOBSharp.Client.Requests;
using SLOBSharp.Client.Responses;
using SLOBSharp.Domain.Services;

namespace SLOBSharp.Client
{
    public enum SlobsClientType
    {
        Pipe,
        WebSocket
    }

    public interface ISlobsClient
    {
        ISlobsConnectionInfo SlobsConnectionInfo { get; }

        SlobsRpcResponse ExecuteRequest(ISlobsRequest request);

        Task<SlobsRpcResponse> ExecuteRequestAsync(ISlobsRequest request);

        IEnumerable<SlobsRpcResponse> ExecuteRequests(IEnumerable<ISlobsRequest> requests);

        IEnumerable<SlobsRpcResponse> ExecuteRequests(params ISlobsRequest[] requests);

        Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(IEnumerable<ISlobsRequest> requests);

        Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(params ISlobsRequest[] requests);
    }

    public interface ISlobsConnectionInfo
    {
        string PipeName { get; }
        SlobsClientType SlobsClientType { get; }
    }

    public abstract class SlobsClient : ISlobsClient
    {
        private readonly ISlobsService slobsService;

        public ISlobsConnectionInfo SlobsConnectionInfo { get; protected set; }

        internal SlobsClient(ISlobsService slobsService)
        {
            this.slobsService = slobsService;
        }

        public SlobsRpcResponse ExecuteRequest(ISlobsRequest request)
        {
            return this.slobsService.ExecuteRequest(request);
        }

        public async Task<SlobsRpcResponse> ExecuteRequestAsync(ISlobsRequest request)
        {
            return await this.slobsService.ExecuteRequestAsync(request).ConfigureAwait(false);
        }

        public IEnumerable<SlobsRpcResponse> ExecuteRequests(params ISlobsRequest[] requests)
        {
            return this.slobsService.ExecuteRequests(requests);
        }

        public IEnumerable<SlobsRpcResponse> ExecuteRequests(IEnumerable<ISlobsRequest> requests)
        {
            return this.slobsService.ExecuteRequests(requests);
        }

        public async Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(params ISlobsRequest[] requests)
        {
            return await this.slobsService.ExecuteRequestsAsync(requests).ConfigureAwait(false);
        }

        public async Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(IEnumerable<ISlobsRequest> requests)
        {
            return await this.slobsService.ExecuteRequestsAsync(requests).ConfigureAwait(false);
        }
    }

    public class SlobsConnectionInfo : ISlobsConnectionInfo
    {
        public string PipeName { get; }

        public SlobsClientType SlobsClientType { get; }

        public SlobsConnectionInfo(SlobsClientType slobsClientType, string pipeName)
        {
            this.SlobsClientType = slobsClientType;
            this.PipeName = pipeName;
        }
    }

    public class SlobsPipeClient : SlobsClient
    {
        public SlobsPipeClient() : this("slobs")
        {
        }

        public SlobsPipeClient(string pipeName) : base(new SlobsPipeService(pipeName))
        {
            this.SlobsConnectionInfo = new SlobsConnectionInfo(SlobsClientType.Pipe, pipeName);
        }
    }
}
