using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/expireat
    public class ExpireAtCommand : RedisCommand
    {
        public ExpireAtCommand(string key, long timestamp) : base(KeysCommandNames.ExpireAt)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(timestamp);
        }
    }
}
