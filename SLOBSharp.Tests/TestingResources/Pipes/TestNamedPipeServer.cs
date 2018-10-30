using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Threading;
using Newtonsoft.Json;
using SLOBSharp.Client.Requests;
using SLOBSharp.Client.Responses;

namespace SLOBSharp.Tests.TestingResources.Pipes
{
    internal class TestNamedPipeServer : IDisposable
    {
        private bool disconnect;
        private bool disposedValue;
        private Thread serverThread;
        private readonly IList<RequestToResponseMapping> responseMappings;
        public string PipeName { get; }

        public TestNamedPipeServer()
        {
            this.responseMappings = new List<RequestToResponseMapping>();
            this.PipeName = Guid.NewGuid().ToString("N");
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public RequestToResponseMapping OnRequest(ISlobsRequest request)
        {
            var mapping = new RequestToResponseMapping(request);
            this.responseMappings.Add(mapping);

            return mapping;
        }

        public void StartServer()
        {
            this.serverThread = new Thread(() =>
            {
                while (!this.disconnect)
                {
                    using (var server = new NamedPipeServerStream(this.PipeName, PipeDirection.InOut))
                    using (var reader = new StreamReader(server))
                    using (var writer = new StreamWriter(server) { NewLine = "\n" })
                    {
                        server.WaitForConnection();
                        for (var i = 0; i < this.responseMappings.Count; i++)
                        {
                            var requestReceivedJson = reader.ReadLine();
                            if (!string.IsNullOrWhiteSpace(requestReceivedJson))
                            {
                                var requestReceived = JsonConvert.DeserializeObject<SlobsRequest>(requestReceivedJson);
                                foreach (var mapping in this.responseMappings)
                                {
                                    if (mapping.Request.Method == requestReceived.Method && mapping.Request.Parameters.Equals(requestReceived.Parameters))
                                    {
                                        var responseJson = JsonConvert.SerializeObject(mapping.Response);
                                        writer.WriteLine(responseJson);
                                    }
                                }
                            }
                        }

                        writer.Flush();
                    }
                }
            });

            this.serverThread.Start();
        }

        public void StopServer()
        {
            this.disconnect = true;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.disconnect = true;
                }

                disposedValue = true;
            }
        }
    }

    internal class RequestToResponseMapping
    {
        public RequestToResponseMapping(ISlobsRequest request)
        {
            this.Request = request;
        }

        public ISlobsRequest Request { get; }
        public SlobsRpcResponse Response { get; private set; }

        public void Return(SlobsRpcResponse slobsRpcResponse)
        {
            this.Response = slobsRpcResponse;
        }
    }
}
