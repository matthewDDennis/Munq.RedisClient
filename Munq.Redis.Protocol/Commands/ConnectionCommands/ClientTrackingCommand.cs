using System;
using System.Collections.Generic;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
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
}
