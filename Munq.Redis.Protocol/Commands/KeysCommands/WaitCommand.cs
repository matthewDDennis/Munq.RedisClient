using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/wait
    public class WaitCommand : RedisCommand
    {
        public WaitCommand(int numReplicas, long milliseconds) : base(KeysCommandNames.Wait)
        {
            AddArgument(numReplicas);
            AddArgument(milliseconds);
        }
    }
}
