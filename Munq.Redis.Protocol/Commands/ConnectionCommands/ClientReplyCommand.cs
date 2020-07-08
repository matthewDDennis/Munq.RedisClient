using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    public enum ClientReplyMode
    {
        OFF,
        ON,
        Skip
    }

    // https://redis.io/commands/client-reply
    public class ClientReplyCommand : RedisCommand
    {
        public ClientReplyCommand(ClientReplyMode replyMode)
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Reply)
        {
            AddArgument(replyMode.ToString());
        }
    }
}
