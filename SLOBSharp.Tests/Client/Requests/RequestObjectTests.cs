using SLOBSharp.Client.Requests;
using SLOBSharp.Client.Requests.Scenes;
using Xunit;

namespace SLOBSharp.Tests.Client.Requests
{
    public class RequestObjectTests
    {
        [Fact]
        public void CanBuildASlobsGetActiveSceneRequest()
        {
            var mockedRequest = new SlobsRequest { Method = "activeScene" };
            mockedRequest.Parameters.SetResource("ScenesService");

            var slobsGetActiveSceneRequest = new SlobsGetActiveSceneRequest();

            Assert.Equal(mockedRequest.Method, slobsGetActiveSceneRequest.Method);
            Assert.True(mockedRequest.Parameters.Equals(slobsGetActiveSceneRequest.Parameters));
        }
    }
}
