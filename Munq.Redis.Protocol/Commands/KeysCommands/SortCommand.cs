using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/sort
    public class SortCommand : RedisCommand
    {
        public SortCommand() : base(KeysCommandNames.Sort)
        {
            throw new NotImplementedException();
        }
    }
}
