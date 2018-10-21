using System.Collections.Generic;
using Newtonsoft.Json;
using SLOBSharp.Domain.Mapping;

namespace SLOBSharp.Tests.TestingResources
{
    public class SingleOrArrayDto
    {
        public SingleOrArrayDto()
        {
            this.Result = new List<string>();
        }

        [JsonProperty("result")]
        [JsonConverter(typeof(SingleOrArrayConverter<string>))]
        public List<string> Result { get; set; }
    }
}
