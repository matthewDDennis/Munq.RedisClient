using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/client-list
    public class ClientListCommand : RedisCommand
    {
        public ClientListCommand()
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.List)
        { }
    }
}
