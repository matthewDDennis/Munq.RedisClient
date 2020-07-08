using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/echo
    public class EchoCommand : RedisCommand
    {
        public EchoCommand(string message) : base(ConnectionCommandNames.Echo)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException($"{nameof(message)} is null or empty.", nameof(message));
            }

            AddArgument(message);
        }
    }
}
