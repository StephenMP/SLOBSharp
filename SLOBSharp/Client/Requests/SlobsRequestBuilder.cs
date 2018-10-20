using System.Collections.Generic;

namespace SLOBSharp.Client.Requests
{
    public interface ISlobsMultipleRequestBuilder : ISlobsRequestBuilderOptions<ISlobsMultipleRequestBuilder>
    {
        IEnumerable<ISlobsRequest> BuildRequests();

        ISlobsMultipleRequestBuilder NextRequest();
    }

    public interface ISlobsRequestBuilder : ISlobsRequestBuilderOptions<ISlobsRequestBuilder>
    {
        ISlobsRequest BuildRequest();
    }

    public interface ISlobsRequestBuilderOptions<TBuilder>
    {
        TBuilder AddArgs(params object[] args);

        TBuilder SetMethod(string method);

        TBuilder SetRequestId(string requestId);

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

        public static ISlobsMultipleRequestBuilder NewRequest() => new SlobsMultipleRequestBuilder();

        public ISlobsMultipleRequestBuilder AddArgs(params object[] args)
        {
            this.currentRequestBuilder.AddArgs(args);
            return this;
        }

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

        public ISlobsMultipleRequestBuilder NextRequest()
        {
            this.requestBuilders.Add(this.currentRequestBuilder);
            this.currentRequestBuilder = new SlobsRequestBuilder();
            return this;
        }

        public ISlobsMultipleRequestBuilder SetMethod(string method)
        {
            this.currentRequestBuilder.SetMethod(method);
            return this;
        }

        public ISlobsMultipleRequestBuilder SetRequestId(string requestId)
        {
            this.currentRequestBuilder.SetRequestId(requestId);
            return this;
        }

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

        public static SlobsRequestBuilder NewRequest() => new SlobsRequestBuilder();

        public ISlobsRequestBuilder AddArgs(params object[] args)
        {
            this.currentRequest.Parameters.AddArgs(args);
            return this;
        }

        public ISlobsRequest BuildRequest()
        {
            return this.currentRequest;
        }

        public ISlobsRequestBuilder SetMethod(string method)
        {
            this.currentRequest.Method = method;
            return this;
        }

        public ISlobsRequestBuilder SetRequestId(string requestId)
        {
            this.currentRequest.Id = requestId;
            return this;
        }

        public ISlobsRequestBuilder SetResource(string resource)
        {
            this.currentRequest.Parameters.Resource = resource;
            return this;
        }
    }
}
