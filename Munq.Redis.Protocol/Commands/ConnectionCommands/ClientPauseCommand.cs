using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/client-pause
    public class ClientPauseCommand : RedisCommand
    {
        public ClientPauseCommand(long timeout)
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Pause)
        {
            AddArgument(timeout);
        }
    }
}
