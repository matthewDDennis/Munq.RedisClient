using System;
using System.Linq;
using System.Text;

namespace Munq.Redis.Protocol.Commands
{
    public enum ClientType
    {
        NoFilter,
        Normal,
        Master,
        Slave,
        Replica, // preferred over Slave but requires Redis 5 or greater.
        PubSub
    }

    // https://redis.io/commands/client-kill
    public class ClientKillCommand : RedisCommand
    {
        private static readonly byte[] AddrFilterName       = Encoding.UTF8.GetBytes("ADDR");
        private static readonly byte[] IdFilterName         = Encoding.UTF8.GetBytes("ID");
        private static readonly byte[] ClientTypeFilterName = Encoding.UTF8.GetBytes("TYPE");
        private static readonly byte[] UsernameFilterName   = Encoding.UTF8.GetBytes("USER");
        private static readonly byte[] SkipMeFilterName     = Encoding.UTF8.GetBytes("SKIPME");

        public ClientKillCommand(string addr = null, string id = null,
                          ClientType clientType = ClientType.NoFilter,
                          string username = null, bool? skipMe = null)
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Kill)
        {
            if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(username)
                && string.IsNullOrWhiteSpace(addr) && username == null)
            {
                throw new ArgumentException("At least one of the addr, id, clientType or username parameters must be non-default.");
            }

            if (!string.IsNullOrWhiteSpace(id))
            {
                AddArgument(IdFilterName);
                AddArgument(id);
            }

            if (clientType != ClientType.NoFilter)
            {
                AddArgument(ClientTypeFilterName);
                AddArgument(clientType.ToString());
            }

            if (!string.IsNullOrWhiteSpace(username))
            {
                AddArgument(UsernameFilterName);
                AddArgument(username);
            }

            if (skipMe != null)
            {
                AddArgument(SkipMeFilterName);
                AddArgument(username);
            }

            if (!string.IsNullOrWhiteSpace(addr))
            {
                if (Parameters.Any())
                    AddArgument(AddrFilterName);

                AddArgument(addr);
            }

            if (string.IsNullOrWhiteSpace(id) && string.IsNullOrWhiteSpace(username)
                && string.IsNullOrWhiteSpace(addr) && username == null)
            {
                throw new ArgumentException("At least one of the parameters must be non-null.");
            }
        }
    }
}
