using System.Linq;
using SLOBSharp.Client;
using SLOBSharp.Client.Requests;
using SLOBSharp.Client.Requests.Scenes;
using SLOBSharp.Domain.Services;
using Xunit;

namespace SLOBSharp.Tests
{
    public class Temp
    {
        [Fact]
        public void Meh()
        {
            var client = new SlobsPipeClient();
            var request = new SlobsGetActiveSceneRequest();
            var result = client.ExecuteRequest(request);
        }

        [Fact]
        public void Meh2()
        {
            var client = new SlobsPipeService();
            var request = SlobsMultipleRequestBuilder.NewRequest().SetResource("ScenesService").SetMethod("getScenes")
                                                     .NextRequest().SetResource("ScenesService").SetMethod("activeScene")
                                                     .NextRequest().SetResource("ScenesService").SetMethod("activeScene")
                                                     .NextRequest().SetResource("ScenesService").SetMethod("activeScene")
                                                     .NextRequest().SetResource("ScenesService").SetMethod("activeScene")
                                                     .NextRequest().SetResource("ScenesService").SetMethod("activeScene").BuildRequests();

            var result = client.ExecuteRequests(request.ToArray());
        }
    }
}
