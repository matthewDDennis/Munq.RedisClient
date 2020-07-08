using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/ping
    public class PingCommand : RedisCommand
    {
        public PingCommand() : base(ConnectionCommandNames.Ping)
        { }

        public PingCommand(string echoString) : base(ConnectionCommandNames.Ping)
        {
            if (string.IsNullOrEmpty(echoString))
            {
                throw new ArgumentException($"{nameof(echoString)} is null or empty.", nameof(echoString));
            }

            AddArgument(echoString);
        }
    }
}
