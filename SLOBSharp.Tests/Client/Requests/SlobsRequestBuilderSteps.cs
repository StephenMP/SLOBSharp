using System.Collections.Generic;
using System.Linq;
using SLOBSharp.Client.Requests;
using Xunit;

namespace SLOBSharp.Tests.Client.Requests
{
    internal class SlobsRequestBuilderSteps
    {
        private SlobsRequest mockedSlobsRequest;
        private SlobsRequestBuilder slobsRequestBuilder;
        private ISlobsRequest slobsRequest;
        private ISlobsMultipleRequestBuilder slobsMultipleRequestBuilder;
        private IEnumerable<ISlobsRequest> slobsRequests;
        private readonly List<ISlobsRequest> mockedRequests;

        public SlobsRequestBuilderSteps()
        {
            this.mockedRequests = new List<ISlobsRequest>();
        }

        internal void GivenIHaveAMockedRequest(string id, string methodName, string resource, params string[] args)
        {
            this.mockedSlobsRequest = new SlobsRequest();
            this.mockedSlobsRequest.Id = id;
            this.mockedSlobsRequest.Method = methodName;
            this.mockedSlobsRequest.Parameters.SetResource(resource);
            this.mockedSlobsRequest.Parameters.AddArgs(args);
        }

        internal void GivenIHaveASlobsRequestBuilder()
        {
            this.slobsRequestBuilder = SlobsRequestBuilder.NewRequest();
        }

        internal void WhenISetTheRequestIdTo(string id)
        {
            this.slobsRequestBuilder.SetRequestId(id);
        }

        internal void WhenISetTheResourceTo(string resource)
        {
            this.slobsRequestBuilder.SetResource(resource);
        }

        internal void ThenTheRequestShouldEqualTheMockedRequest()
        {
            Assert.Equal(this.mockedSlobsRequest.Id, this.slobsRequest.Id);
            Assert.Equal(this.mockedSlobsRequest.Method, this.slobsRequest.Method);
            Assert.True(this.mockedSlobsRequest.Parameters.Equals(this.slobsRequest.Parameters));
        }

        internal void ThenIShouldHaveARequest()
        {
            Assert.NotNull(this.slobsRequest);
        }

        internal void WhenIBuildTheRequest()
        {
            this.slobsRequest = this.slobsRequestBuilder.BuildRequest();
        }

        internal void WhenIAddTheArgs(params string[] args)
        {
            this.slobsRequestBuilder.AddArgs(args);
        }

        internal void WhenISetTheMethodTo(string methodName)
        {
            this.slobsRequestBuilder.SetMethod(methodName);
        }

        internal void GivenIHaveMultipleMockedRequest(string id, string methodName, string resource, int numberOfRequests, params string[] args)
        {
            for (var i = 0; i < numberOfRequests; i++)
            {
                var mockedSlobsRequest = new SlobsRequest();
                mockedSlobsRequest.Id = id;
                mockedSlobsRequest.Method = methodName;
                mockedSlobsRequest.Parameters.SetResource(resource);
                mockedSlobsRequest.Parameters.AddArgs(args);

                this.mockedRequests.Add(mockedSlobsRequest);
            }
        }

        internal void GivenIHaveASlobsMultipleRequestBuilder()
        {
            this.slobsMultipleRequestBuilder = SlobsMultipleRequestBuilder.NewRequest();
        }

        internal void WhenISetMultipleRequestIdsTo(string id)
        {
            this.slobsMultipleRequestBuilder.SetRequestId(id);
        }

        internal void WhenISetMultipleMethodsTo(string methodName)
        {
            this.slobsMultipleRequestBuilder.SetMethod(methodName);
        }

        internal void WhenIAddMultipleArgs(params string[] arg)
        {
            this.slobsMultipleRequestBuilder.AddArgs(arg);
        }

        internal void WhenIBeginTheNextRequest()
        {
            this.slobsMultipleRequestBuilder.NextRequest();
        }

        internal void ThenTheRequestsShouldEqualTheMockedRequests()
        {
            for (var i = 0; i < this.mockedRequests.Count; i++)
            {
                Assert.Equal(this.mockedRequests[i].Id, this.slobsRequests.ElementAt(i).Id);
                Assert.Equal(this.mockedRequests[i].Method, this.slobsRequests.ElementAt(i).Method);
                Assert.True(this.mockedRequests[i].Parameters.Equals(this.slobsRequests.ElementAt(i).Parameters));
            }
        }

        internal void ThenIShouldHaveRequests()
        {
            Assert.NotNull(this.slobsRequests);
            Assert.NotEmpty(this.slobsRequests);
        }

        internal void WhenIBuildTheRequests()
        {
            this.slobsRequests = this.slobsMultipleRequestBuilder.BuildRequests();
        }

        internal void WhenISetMultipleResourcesTo(string resource)
        {
            this.slobsMultipleRequestBuilder.SetResource(resource);
        }
    }
}