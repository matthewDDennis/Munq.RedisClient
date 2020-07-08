using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/expire
    public class ExpireCommand : RedisCommand
    {
        public ExpireCommand(string key, int seconds) : base(KeysCommandNames.Expire)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(seconds);
        }
    }
}
