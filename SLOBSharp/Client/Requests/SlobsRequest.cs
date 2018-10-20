using System;
using Newtonsoft.Json;

namespace SLOBSharp.Client.Requests
{
    public interface ISlobsRequest
    {
        string Id { get; }
        string JsonRpc { get; }
        string Method { get; }
        SlobsParameters Parameters { get; }

        string ToJson();
    }

    public class SlobsRequest : ISlobsRequest
    {
        [JsonProperty("id")]
        public string Id { get; internal set; }

        [JsonProperty("jsonrpc")]
        public string JsonRpc => "2.0";

        [JsonProperty("method")]
        public string Method { get; internal set; }

        [JsonProperty("params")]
        public SlobsParameters Parameters { get; internal set; }

        public SlobsRequest() : this(Guid.NewGuid().ToString("N"))
        {
        }

        public SlobsRequest(string id)
        {
            this.Id = id;
            this.Parameters = new SlobsParameters();
        }

        public string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
