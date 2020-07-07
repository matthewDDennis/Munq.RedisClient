﻿using System;
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
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"{nameof(password)} is null or empty.", nameof(password));
            }
        }

        public AuthCommand(string username, string password) 
            : base(ConnectionCommandNames.Auth, username, password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"{nameof(password)} is null or empty.", nameof(password));
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException($"{nameof(username)} is null or empty.", nameof(username));
            }
        }
    }

    // https://redis.io/commands/client-caching
    public class ClientCachingCommand : RedisCommand
    {
        public ClientCachingCommand(bool enable)
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Client, enable ? "YES" : "NO")
        {}
    }

    // https://redis.io/commands/client-getname
    public class ClientGetNameCommand : RedisCommand
    {
        public ClientGetNameCommand()
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.GetName)
        {}
    }

    // https://redis.io/commands/client-getredir
    public class ClientGetRedirCommand : RedisCommand
    {
        public ClientGetRedirCommand()
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.GetRedir)
        { }
    }

    // https://redis.io/commands/client-id
    public class ClientIdCommand : RedisCommand
    {
        public ClientIdCommand()
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

    // https://redis.io/commands/client-list
    public class ClientListCommand : RedisCommand
    {
        public ClientListCommand() 
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.List)
        {}
    }

    // https://redis.io/commands/client-pause
    public class ClientPauseCommand : RedisCommand
    {
        public ClientPauseCommand(long timeout) 
            : base(ConnectionCommandNames.Client, ConnectionCommandNames.Pause)
        {
            AddArgument(timeout);
        }
    }

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

    // https://redis.io/commands/client-tracking
    public class ClientTrackingCommand : RedisCommand
    {
        public ClientTrackingCommand(bool enable, string redirectClientId = null,
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
    public class ClientUnblockCommand : RedisCommand
    {
        public ClientUnblockCommand(string clientId, bool unblockAsError)
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
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException($"{nameof(message)} is null or empty.", nameof(message));
            }

            AddArgument(message);
        }
    }

    // https://redis.io/commands/hello
    public class HelloCommand : RedisCommand
    {
        public HelloCommand(int protoVer, string username = null, string password = null, 
                            string clientName = null) : base(ConnectionCommandNames.Hello)
        {
            AddArgument(protoVer);
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

        public PingCommand(string echoString) : base(ConnectionCommandNames.Ping)
        {
            if (string.IsNullOrEmpty(echoString))
            {
                throw new ArgumentException($"{nameof(echoString)} is null or empty.", nameof(echoString));
            }

            AddArgument(echoString);
        }
    }

    // https://redis.io/commands/ping
    public class QuitCommand : RedisCommand
    {
        public QuitCommand() : base(ConnectionCommandNames.Quit)
        {}
    }

    // https://redis.io/commands/select
    public class SelectCommand : RedisCommand
    {
        public SelectCommand(int databaseNum) : base(ConnectionCommandNames.Select)
        {
            AddArgument(databaseNum);
        }
    }
}
