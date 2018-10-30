using SLOBSharp.Domain.Services;

namespace SLOBSharp.Client
{
    public class SlobsPipeClient : SlobsClient
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SlobsPipeClient"/> class using "slobs" as the default pipe name.
        /// </summary>
        public SlobsPipeClient() : this("slobs")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SlobsPipeClient"/> class.
        /// </summary>
        /// <param name="pipeName">Name of the pipe.</param>
        public SlobsPipeClient(string pipeName) : this(new SlobsPipeService(pipeName))
        {
        }

        internal SlobsPipeClient(ISlobsService slobsService) : base(slobsService)
        {
        }
    }
}
