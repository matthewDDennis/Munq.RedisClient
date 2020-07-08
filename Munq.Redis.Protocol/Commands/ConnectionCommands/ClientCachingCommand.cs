using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/client-caching
    public class ClientCachingCommand : RedisCommand
    {
        public ClientCachingCommand(bool enable)
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Client, enable ? "YES" : "NO")
        { }
    }
}
