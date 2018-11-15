using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace SLOBSharp.Client.Requests
{
    public class SlobsParameters : IEquatable<SlobsParameters>
    {
        [JsonProperty("args")]
        public List<object> Args { get; set; }

        [JsonProperty("resource")]
        public string Resource { get; set; }

        public SlobsParameters()
        {
            this.Args = new List<object>();
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

        public bool Equals(SlobsParameters other)
        {
            var equal = this.Resource == other.Resource && this.Args.Count == other.Args.Count;

            for (var i = 0; i < this.Args.Count; i++)
            {
                equal = equal && this.Args[i].Equals(other.Args[i]);
            }

            return equal;
        }
    }
}
