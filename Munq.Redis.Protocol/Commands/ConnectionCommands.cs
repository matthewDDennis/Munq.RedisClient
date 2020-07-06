using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/auth
    public class AuthCommand : RedisCommand
    {
        public AuthCommand(string password) : base(ConnectionCommandNames.Auth, password)
        {}

        public AuthCommand(string username, string password) 
            : base(ConnectionCommandNames.Auth, username, password)
        {}
    }

    // https://redis.io/commands/client-caching
    public class ClientCaching : RedisCommand
    {
        public ClientCaching(bool enable)
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Client, enable ? "YES" : "NO")
        {}
    }

    // https://redis.io/commands/client-getname
    public class ClientGetName : RedisCommand
    {
        public ClientGetName()
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.GetName)
        {}
    }

    // https://redis.io/commands/client-getredir
    public class ClientGetRedir : RedisCommand
    {
        public ClientGetRedir()
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.GetRedir)
        { }
    }

    // https://redis.io/commands/client-id
    public class ClientId : RedisCommand
    {
        public ClientId()
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.ID)
        { }
    }

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
    public class ClientKill : RedisCommand
    {
        private static readonly byte[] AddrFilterName       = Encoding.UTF8.GetBytes("ADDR");
        private static readonly byte[] IdFilterName         = Encoding.UTF8.GetBytes("ID");
        private static readonly byte[] ClientTypeFilterName = Encoding.UTF8.GetBytes("TYPE");
        private static readonly byte[] UsernameFilterName   = Encoding.UTF8.GetBytes("USER");
        private static readonly byte[] SkipMeFilterName     = Encoding.UTF8.GetBytes("SKIPME");

        public ClientKill(string addr = null, string id = null, 
                          ClientType clientType = ClientType.NoFilter,
                          string username = null, bool? skipMe = null)
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Kill)
        {
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
        }
    }

    // https://redis.io/commands/client-list
    public class ClientList : RedisCommand
    {
        public ClientList() 
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.List)
        {}
    }

    // https://redis.io/commands/client-pause
    public class ClientPause : RedisCommand
    {
        public ClientPause(ulong timeout) 
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Pause)
        {
            AddArgument(timeout.ToString());
        }
    }

    public enum ClientReplyMode
    {
        OFF,
        ON,
        Skip
    }

    // https://redis.io/commands/client-reply
    public class ClientReply : RedisCommand
    {
        public ClientReply(ClientReplyMode replyMode) 
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Reply)
        {
            AddArgument(replyMode.ToString());
        }
    }

    // https://redis.io/commands/client-setname
    public class ClientSetName : RedisCommand
    {
        public ClientSetName(string connectionName) 
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.SetName)
        {
            AddArgument(connectionName);
        }
    }

    // https://redis.io/commands/client-tracking
    public class ClientTracking : RedisCommand
    {
        public ClientTracking(bool enable, string redirectClientId = null,
                           IEnumerable<string> prefixes = null,
                           bool bcast = false, bool optIn = false,
                           bool optOut = false, bool noLoop = false)
           : base(ConnectionCommandNames.Client, ConnectionCommandNames.Tracking)
        {
            AddArgument(enable ? "ON" : "OFF");

            if (redirectClientId != null)
            {
                AddArgument("REDIRECT");
                AddArgument(redirectClientId);
            }

            if (prefixes?.Any() ?? false)
            {
                foreach (var prefix in prefixes)
                {
                    AddArgument("PREFIX");
                    AddArgument(prefix);
                }
            }

            if (bcast)
                AddArgument("BCAST");

            if (optIn)
                AddArgument("OPTIN");

            if (optOut)
                AddArgument("OPTOUT");

            if (noLoop)
                AddArgument("NOLOOP");
        }
    }

    // https://redis.io/commands/client-unblock
    public class ClientUnblock : RedisCommand
    {
        public ClientUnblock(string clientId, bool unblockAsError)
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Unblock)
        {
            if (unblockAsError)
                AddArgument("ERROR");
        }
    }

    // https://redis.io/commands/echo
    public class EchoCommand : RedisCommand
    {
        public EchoCommand(string message) : base(ConnectionCommandNames.Echo)
        {
            AddArgument(message);
        }
    }

    // https://redis.io/commands/hello
    public class HelloCommand : RedisCommand
    {
        public HelloCommand(int protoVer, string username = null, string password = null, 
                            string clientName = null) : base(ConnectionCommandNames.Hello)
        {
            AddArgument(protoVer.ToString());
            if (username != null ^ password == null)
                throw new ArgumentException("username and password must both contain values or both be null");

            if (username != null)
            {
                AddArgument("AUTH");
                AddArgument(username);
                AddArgument(password);
            }

            if (clientName != null)
            {
                AddArgument("SETNAME");
                AddArgument(clientName);
            }
        }
    }

    // https://redis.io/commands/ping
    public class PingCommand : RedisCommand
    {
        public PingCommand() : base(ConnectionCommandNames.Ping)
        {}

        public PingCommand(string echoString) : base(ConnectionCommandNames.Ping, echoString)
        {}
    }

    // https://redis.io/commands/ping
    public class QuitCommand : RedisCommand
    {
        public QuitCommand() : base(ConnectionCommandNames.Quit)
        {}
    }

    // https://redis.io/commands/select
    public class Select : RedisCommand
    {
        public Select(int databaseNum) : base(ConnectionCommandNames.Select)
        {
            AddArgument(databaseNum.ToString());
        }
    }
}
