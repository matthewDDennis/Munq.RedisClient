using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/client-setname
    public class ClientSetNameCommand : RedisCommand
    {
        public ClientSetNameCommand(string connectionName)
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.SetName)
        {
            if (connectionName == null)
            {
                throw new ArgumentNullException(nameof(connectionName), $"{nameof(connectionName)} is null.");
            }

            AddArgument(connectionName);
        }
    }
}
