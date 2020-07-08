using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/client-getname
    public class ClientGetNameCommand : RedisCommand
    {
        public ClientGetNameCommand()
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.GetName)
        { }
    }
}
