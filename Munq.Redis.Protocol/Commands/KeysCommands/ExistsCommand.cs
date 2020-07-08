using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/exists
    public class ExistsCommand : RedisCommand
    {
        public ExistsCommand(params string[] keys) : base(KeysCommandNames.Delete)
        {
            if ((keys == null) || (keys.Length == 0))
            {
                throw new ArgumentException($"{nameof(keys)} is null or empty.", nameof(keys));
            }

            AddArguments(keys);
        }

    }
}
