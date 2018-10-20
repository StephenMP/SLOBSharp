using System.Collections.Generic;
using Newtonsoft.Json;

namespace SLOBSharp.Client.Requests
{
    public class SlobsParameters
    {
        [JsonProperty("args")]
        public List<object> Args { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        public SlobsParameters()
        {
            this.Args = new List<object>();
        }

        public SlobsParameters(string resource) : this()
        {
            this.Resource = resource;
        }

        public SlobsParameters AddArgs(params object[] args)
        {
            this.Args.AddRange(args);
            return this;
        }

        public SlobsParameters SetResource(string resource)
        {
            this.Resource = resource;
            return this;
        }
    }
}
