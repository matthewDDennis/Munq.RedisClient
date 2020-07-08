using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/ping
    public class QuitCommand : RedisCommand
    {
        public QuitCommand() : base(ConnectionCommandNames.Quit)
        { }
    }
}
