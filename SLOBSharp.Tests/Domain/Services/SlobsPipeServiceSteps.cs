using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SLOBSharp.Client.Requests;
using SLOBSharp.Client.Responses;
using SLOBSharp.Domain.Services;
using Xunit;

namespace SLOBSharp.Tests.Domain.Services
{
    internal class SlobsPipeServiceSteps : IDisposable
    {
        private ISlobsRequest[] slobsReqests;
        private StreamReader mockedStreamReader;
        private MemoryStream readerMemoryStream;
        private StreamWriter readerMemoryStreamWriter;
        private readonly List<SlobsRpcResponse> mockedSlobsRpcResponses;

        public SlobsPipeServiceSteps()
        {
            this.mockedSlobsRpcResponses = new List<SlobsRpcResponse>();
        }

        internal void GivenIHaveSlobsRequests(int numberOfRequests)
        {
            var slobsRequests = new List<ISlobsRequest>();
            for (var i = 0; i < numberOfRequests; i++)
            {
                var arg = Guid.NewGuid().ToString("N");
                var method = Guid.NewGuid().ToString("N");
                var requestId = Guid.NewGuid().ToString("N");
                var resource = Guid.NewGuid().ToString("N");
                var slobsRequest = SlobsRequestBuilder.NewRequest().AddArgs(arg).SetMethod(method).SetRequestId(requestId).SetResource(resource).BuildRequest();

                slobsRequests.Add(slobsRequest);
            }

            this.slobsReqests = slobsRequests.ToArray();
        }

        internal void ThenIShouldReceiveAResponse()
        {
            Assert.NotNull(this.slobsRpcResponses);
            Assert.NotEmpty(this.slobsRpcResponses);
        }

        internal void WhenICallExecuteRequests()
        {
            this.slobsRpcResponses = this.slobsPipeService.ExecuteRequests(null, this.mockedStreamReader, this.mockedStreamWriter, this.slobsReqests);
        }

        internal async Task WhenICallExecuteRequestsAsync()
        {
            this.slobsRpcResponses = await this.slobsPipeService.ExecuteRequestsAsync(null, this.mockedStreamReader, this.mockedStreamWriter, this.slobsReqests).ConfigureAwait(false);
        }

        internal void GivenIHaveASlobsPipeService()
        {
            this.slobsPipeService = new SlobsPipeService();
        }

        internal void GivenIHaveAMockedStreamWriter()
        {
            this.writerMemoryStream = new MemoryStream();
            this.mockedStreamWriter = new StreamWriter(this.writerMemoryStream);
        }

        internal void GivenIHaveAMockedStreamReader()
        {
            this.readerMemoryStream = new MemoryStream();
            this.readerMemoryStreamWriter = new StreamWriter(readerMemoryStream);
            foreach (var mockedSlobsRpcResponse in this.mockedSlobsRpcResponses)
            {
                this.readerMemoryStreamWriter.WriteLine(JsonConvert.SerializeObject(mockedSlobsRpcResponse));
            }

            this.readerMemoryStreamWriter.Flush();
            readerMemoryStream.Position = 0;

            this.mockedStreamReader = new StreamReader(readerMemoryStream);
        }

        internal void GivenIHaveMultipleMockedResponse()
        {
            foreach (var slobsRequest in this.slobsReqests)
            {
                var slobsResult = new SlobsResult { Id = slobsRequest.Id };
                var slobsRpcResponse = new SlobsRpcResponse { Result = new[] { slobsResult } };
                this.mockedSlobsRpcResponses.Add(slobsRpcResponse);
            }
        }

        #region IDisposable Support
        private bool disposedValue;
        private MemoryStream writerMemoryStream;
        private StreamWriter mockedStreamWriter;
        private SlobsPipeService slobsPipeService;
        private IEnumerable<SlobsRpcResponse> slobsRpcResponses;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.mockedStreamReader?.Dispose();
                    this.writerMemoryStream.Dispose();

                    this.mockedStreamReader?.Dispose();
                    this.readerMemoryStreamWriter?.Dispose();
                    this.readerMemoryStream?.Dispose();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}