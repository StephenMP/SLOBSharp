using System;

namespace SLOBSharp.Client.Requests.Scenes
{
    public class SlobsGetActiveSceneRequest : SlobsRequest
    {
        public SlobsGetActiveSceneRequest() : this(Guid.NewGuid().ToString("N"))
        {
        }

        public SlobsGetActiveSceneRequest(string id) : base(id)
        {
            this.Method = "activeScene";
            this.Parameters.Resource = "ScenesService";
        }
    }
}
