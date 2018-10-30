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

    public abstract class SlobsClient : ISlobsClient
    {
        private readonly ISlobsService slobsService;

        internal SlobsClient(ISlobsService slobsService)
        {
            this.slobsService = slobsService;
        }

        public static ISlobsClient NewPipeClient() => new SlobsPipeClient();

        public static ISlobsClient NewPipeClient(string pipeName) => new SlobsPipeClient(pipeName);

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
}
