using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SLOBSharp.Client.Requests;
using SLOBSharp.Client.Responses;

namespace SLOBSharp.Domain.Services
{
    public interface ISlobsService
    {
        SlobsRpcResponse ExecuteRequest(ISlobsRequest request);

        Task<SlobsRpcResponse> ExecuteRequestAsync(ISlobsRequest request);

        IEnumerable<SlobsRpcResponse> ExecuteRequests(IEnumerable<ISlobsRequest> requests);

        IEnumerable<SlobsRpcResponse> ExecuteRequests(params ISlobsRequest[] requests);

        Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(IEnumerable<ISlobsRequest> requests);

        Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(params ISlobsRequest[] requests);
    }

    public class SlobsPipeService : ISlobsService
    {
        private readonly string pipeName;

        public SlobsPipeService() : this("slobs")
        {
        }

        public SlobsPipeService(string pipeName)
        {
            this.pipeName = pipeName;
        }

        public SlobsRpcResponse ExecuteRequest(ISlobsRequest request)
        {
            return this.ExecuteRequests(request)?.FirstOrDefault();
        }

        public async Task<SlobsRpcResponse> ExecuteRequestAsync(ISlobsRequest request)
        {
            return (await this.ExecuteRequestsAsync(request).ConfigureAwait(false))?.FirstOrDefault();
        }

        public IEnumerable<SlobsRpcResponse> ExecuteRequests(NamedPipeClientStream pipe, StreamReader reader, StreamWriter writer, ISlobsRequest[] requests)
        {
            // We have to process 5 or less commands at a time since SLOBS can't handle more than that
            var skip = 5;
            var responseJson = string.Empty;
            var requestsChunk = requests.Take(5);
            var response = default(SlobsRpcResponse);
            var slobsRpcResponses = new List<SlobsRpcResponse>(requests.Length);
            while (requestsChunk.Any())
            {
                foreach (var request in requestsChunk)
                {
                    var requestJson = request.ToJson();
                    writer.WriteLine(request.ToJson());
                }

                writer.Flush();
                pipe?.WaitForPipeDrain();

                for (var i = 0; i < requestsChunk.Count(); i++)
                {
                    responseJson = reader.ReadLine();
                    response = JsonConvert.DeserializeObject<SlobsRpcResponse>(responseJson);
                    response.JsonResponse = responseJson;
                    slobsRpcResponses.Add(response);
                }

                requestsChunk = requests.Skip(skip).Take(5);
                skip += 5;
            }

            return slobsRpcResponses;
        }

        public IEnumerable<SlobsRpcResponse> ExecuteRequests(params ISlobsRequest[] requests)
        {
            using (var pipe = new NamedPipeClientStream(this.pipeName))
            using (var reader = new StreamReader(pipe))
            using (var writer = new StreamWriter(pipe) { NewLine = "\n" })
            {
                pipe.Connect(5000);
                return this.ExecuteRequests(pipe, reader, writer, requests);
            }
        }

        public IEnumerable<SlobsRpcResponse> ExecuteRequests(IEnumerable<ISlobsRequest> requests)
        {
            return this.ExecuteRequests(requests.ToArray());
        }

        public async Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(NamedPipeClientStream pipe, StreamReader reader, StreamWriter writer, ISlobsRequest[] requests)
        {
            // We have to process 5 or less commands at a time since SLOBS can't handle more than that
            var skip = 5;
            var responseJson = string.Empty;
            var requestsChunk = requests.Take(5);
            var response = default(SlobsRpcResponse);
            var slobsRpcResponses = new List<SlobsRpcResponse>(requests.Length);
            while (requestsChunk.Any())
            {
                foreach (var request in requestsChunk)
                {
                    await writer.WriteLineAsync(request.ToJson()).ConfigureAwait(false);
                }

                await writer.FlushAsync().ConfigureAwait(false);
                pipe?.WaitForPipeDrain();

                for (var i = 0; i < requestsChunk.Count(); i++)
                {
                    responseJson = await reader.ReadLineAsync().ConfigureAwait(false);
                    response = JsonConvert.DeserializeObject<SlobsRpcResponse>(responseJson);
                    response.JsonResponse = responseJson;
                    slobsRpcResponses.Add(response);
                }

                requestsChunk = requests.Skip(skip).Take(5);
                skip += 5;
            }

            return slobsRpcResponses;
        }

        public async Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(params ISlobsRequest[] requests)
        {
            using (var pipe = new NamedPipeClientStream(this.pipeName))
            using (var reader = new StreamReader(pipe))
            using (var writer = new StreamWriter(pipe) { NewLine = "\n" })
            {
                await pipe.ConnectAsync(5000).ConfigureAwait(false);
                return await this.ExecuteRequestsAsync(pipe, reader, writer, requests).ConfigureAwait(false);
            }
        }

        public async Task<IEnumerable<SlobsRpcResponse>> ExecuteRequestsAsync(IEnumerable<ISlobsRequest> requests)
        {
            return await this.ExecuteRequestsAsync(requests.ToArray()).ConfigureAwait(false);
        }
    }
}
