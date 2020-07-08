using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/client-unblock
    public class ClientUnblockCommand : RedisCommand
    {
        public ClientUnblockCommand(string clientId, bool unblockAsError)
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Unblock)
        {
            if (unblockAsError)
                AddArgument("ERROR");
        }
    }
}
