using System;
using System.IO.Pipelines;
using System.Threading;
using System.Threading.Tasks;

namespace Munq.RedisClient
{
    internal class RedisResponseReader
    {
        private PipeReader _reader;

        public RedisResponseReader(PipeReader reader)
        {
            _reader = reader;
        }

        public ValueTask<IRedisResponse> ReadResponse(CancellationToken token)
        {
            throw new NotImplementedException();
        }
    }
}