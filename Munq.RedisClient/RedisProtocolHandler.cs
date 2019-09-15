using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Munq.RedisClient
{
    public class RedisProtocolHandler
    {
        private readonly RedisResponseReader _responseReader;
        /// <summary>
        /// Gets the Transport Connection's Application side PipeReader and PipeWriter pair
        /// to which the protocol handler is connected.
        /// </summary>
        public IDuplexPipe Transport { get; }

        /// <summary>
        /// Initialize a new instance of the <see cref="RedisProtocolHandler"/> class.
        /// </summary>
        /// <param name="transport">The Transport Connection's PipeReader and PipeWriter pair.</param>
        public RedisProtocolHandler(IDuplexPipe transport)
        {
            Transport = transport;
            _responseReader = new RedisResponseReader(transport.Input);
        }

        /// <summary>
        /// Serializes the command to the Transport PipeWriter.
        /// </summary>
        /// <param name="redisCommand">The command to serialize.</param>
        /// <returns></returns>
        public ValueTask<FlushResult> WriteCommand(IRedisCommand redisCommand, CancellationToken token = default)
        {
            return Transport.Output.Write(redisCommand, token);
        }

        public ValueTask<IRedisResponse> ReadResponse(CancellationToken token = default)
        {
            return _responseReader.ReadResponse(token);
        }
    }
}
