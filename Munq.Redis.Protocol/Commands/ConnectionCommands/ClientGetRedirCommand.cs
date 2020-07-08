using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/client-getredir
    public class ClientGetRedirCommand : RedisCommand
    {
        public ClientGetRedirCommand()
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.GetRedir)
        { }
    }
}
