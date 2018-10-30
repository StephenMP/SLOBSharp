using System.Collections.Generic;

namespace SLOBSharp.Client.Requests
{
    public interface ISlobsMultipleRequestBuilder : ISlobsRequestBuilderOptions<ISlobsMultipleRequestBuilder>
    {
        /// <summary>
        /// Builds the requests.
        /// </summary>
        /// <returns></returns>
        IEnumerable<ISlobsRequest> BuildRequests();

        /// <summary>
        /// Finishes the current request and begins a new one.
        /// </summary>
        /// <returns></returns>
        ISlobsMultipleRequestBuilder NextRequest();
    }

    public interface ISlobsRequestBuilder : ISlobsRequestBuilderOptions<ISlobsRequestBuilder>
    {
        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns></returns>
        ISlobsRequest BuildRequest();
    }

    public interface ISlobsRequestBuilderOptions<TBuilder>
    {
        /// <summary>
        /// Adds the arguments to the args field in the request parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        TBuilder AddArgs(params object[] args);

        /// <summary>
        /// Sets the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        TBuilder SetMethod(string method);

        /// <summary>
        /// Sets the request identifier.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <returns></returns>
        TBuilder SetRequestId(string requestId);

        /// <summary>
        /// Sets the resource field in the request parameters.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        TBuilder SetResource(string resource);
    }

    public sealed class SlobsMultipleRequestBuilder : ISlobsMultipleRequestBuilder
    {
        private readonly IList<ISlobsRequestBuilder> requestBuilders;

        private ISlobsRequestBuilder currentRequestBuilder;

        public SlobsMultipleRequestBuilder()
        {
            this.requestBuilders = new List<ISlobsRequestBuilder>();
            this.currentRequestBuilder = new SlobsRequestBuilder();
        }

        /// <summary>
        /// Begin a new request.
        /// </summary>
        /// <returns></returns>
        public static ISlobsMultipleRequestBuilder NewRequest() => new SlobsMultipleRequestBuilder();

        /// <summary>
        /// Adds the arguments to the args field in the request parameters.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public ISlobsMultipleRequestBuilder AddArgs(params object[] args)
        {
            this.currentRequestBuilder.AddArgs(args);
            return this;
        }

        /// <summary>
        /// Builds the requests.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ISlobsRequest> BuildRequests()
        {
            this.requestBuilders.Add(this.currentRequestBuilder);
            var results = new List<ISlobsRequest>(this.requestBuilders.Count);
            foreach (var builder in this.requestBuilders)
            {
                results.Add(builder.BuildRequest());
            }

            return results;
        }

        /// <summary>
        /// Finishes the current request and begins a new one.
        /// </summary>
        /// <returns></returns>
        public ISlobsMultipleRequestBuilder NextRequest()
        {
            this.requestBuilders.Add(this.currentRequestBuilder);
            this.currentRequestBuilder = new SlobsRequestBuilder();
            return this;
        }

        /// <summary>
        /// Sets the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public ISlobsMultipleRequestBuilder SetMethod(string method)
        {
            this.currentRequestBuilder.SetMethod(method);
            return this;
        }

        /// <summary>
        /// Sets the request identifier.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <returns></returns>
        public ISlobsMultipleRequestBuilder SetRequestId(string requestId)
        {
            this.currentRequestBuilder.SetRequestId(requestId);
            return this;
        }

        /// <summary>
        /// Sets the resource field in the request parameters.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public ISlobsMultipleRequestBuilder SetResource(string resource)
        {
            this.currentRequestBuilder.SetResource(resource);
            return this;
        }
    }

    public sealed class SlobsRequestBuilder : ISlobsRequestBuilder
    {
        private readonly SlobsRequest currentRequest;

        public SlobsRequestBuilder()
        {
            this.currentRequest = new SlobsRequest();
        }

        /// <summary>
        /// Begin a new request.
        /// </summary>
        /// <returns></returns>
        public static SlobsRequestBuilder NewRequest() => new SlobsRequestBuilder();

        /// <summary>
        /// Adds the arguments to the args field in the parameters object.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns></returns>
        public ISlobsRequestBuilder AddArgs(params object[] args)
        {
            this.currentRequest.Parameters.AddArgs(args);
            return this;
        }

        /// <summary>
        /// Builds the request.
        /// </summary>
        /// <returns></returns>
        public ISlobsRequest BuildRequest()
        {
            return this.currentRequest;
        }

        /// <summary>
        /// Sets the method.
        /// </summary>
        /// <param name="method">The method.</param>
        /// <returns></returns>
        public ISlobsRequestBuilder SetMethod(string method)
        {
            this.currentRequest.Method = method;
            return this;
        }

        /// <summary>
        /// Sets the request identifier.
        /// </summary>
        /// <param name="requestId">The request identifier.</param>
        /// <returns></returns>
        public ISlobsRequestBuilder SetRequestId(string requestId)
        {
            this.currentRequest.Id = requestId;
            return this;
        }

        /// <summary>
        /// Sets the resource field in the parameters object.
        /// </summary>
        /// <param name="resource">The resource.</param>
        /// <returns></returns>
        public ISlobsRequestBuilder SetResource(string resource)
        {
            this.currentRequest.Parameters.Resource = resource;
            return this;
        }
    }
}
