using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/client-id
    public class ClientIdCommand : RedisCommand
    {
        public ClientIdCommand()
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.ID)
        { }
    }
}
