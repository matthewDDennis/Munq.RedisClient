using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
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
}
