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
        /// <summary>
        /// Executes a single request synchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        SlobsRpcResponse ExecuteRequest(ISlobsRequest request);

        /// <summary>
        /// Executes a single request asynchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        Task<SlobsRpcResponse> ExecuteRequestAsync(ISlobsRequest request);

        /// <summary>
        /// Executes multiple requests synchronously.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        IEnumerable<SlobsRpcResponse> ExecuteRequests(params ISlobsRequest[] requests);

        /// <summary>
        /// Executes multiple requests synchronously.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        IEnumerable<SlobsRpcResponse> ExecuteRequests(IEnumerable<ISlobsRequest> requests);

        /// <summary>
        /// Executes multiple requests asynchronously.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(params ISlobsRequest[] requests);

        /// <summary>
        /// Executes multiple requests asynchronously.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(IEnumerable<ISlobsRequest> requests);
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

        /// <summary>
        /// Executes a single request synchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public SlobsRpcResponse ExecuteRequest(ISlobsRequest request)
        {
            return this.slobsService.ExecuteRequest(request);
        }

        /// <summary>
        /// Executes a single request asynchronously.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public async Task<SlobsRpcResponse> ExecuteRequestAsync(ISlobsRequest request)
        {
            return await this.slobsService.ExecuteRequestAsync(request).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes multiple requests synchronously.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        public IEnumerable<SlobsRpcResponse> ExecuteRequests(params ISlobsRequest[] requests)
        {
            return this.slobsService.ExecuteRequests(requests);
        }

        /// <summary>
        /// Executes multiple requests synchronously.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        public IEnumerable<SlobsRpcResponse> ExecuteRequests(IEnumerable<ISlobsRequest> requests)
        {
            return this.slobsService.ExecuteRequests(requests);
        }

        /// <summary>
        /// Executes multiple requests asynchronously.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        public async Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(params ISlobsRequest[] requests)
        {
            return await this.slobsService.ExecuteRequestsAsync(requests).ConfigureAwait(false);
        }

        /// <summary>
        /// Executes multiple requests asynchronously.
        /// </summary>
        /// <param name="requests">The requests.</param>
        /// <returns></returns>
        public async Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(IEnumerable<ISlobsRequest> requests)
        {
            return await this.slobsService.ExecuteRequestsAsync(requests).ConfigureAwait(false);
        }
    }

    public class SlobsConnectionInfo : ISlobsConnectionInfo
    {
        public string PipeName { get; }

        public SlobsClientType SlobsClientType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlobsConnectionInfo"/> class.
        /// </summary>
        /// <param name="slobsClientType">Type of SLOBS client.</param>
        /// <param name="pipeName">Name of the pipe.</param>
        public SlobsConnectionInfo(SlobsClientType slobsClientType, string pipeName)
        {
            this.SlobsClientType = slobsClientType;
            this.PipeName = pipeName;
        }
    }

    public class SlobsPipeClient : SlobsClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlobsPipeClient"/> class using "slobs" as the default pipe name.
        /// </summary>
        public SlobsPipeClient() : this("slobs")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlobsPipeClient"/> class.
        /// </summary>
        /// <param name="pipeName">Name of the pipe.</param>
        public SlobsPipeClient(string pipeName) : base(new SlobsPipeService(pipeName))
        {
            this.SlobsConnectionInfo = new SlobsConnectionInfo(SlobsClientType.Pipe, pipeName);
        }
    }
}