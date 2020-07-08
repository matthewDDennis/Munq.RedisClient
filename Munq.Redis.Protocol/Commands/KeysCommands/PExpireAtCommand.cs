using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/pexpireat
    public class PExpireAtCommand : RedisCommand
    {
        public PExpireAtCommand(string key, long millisecondsTimestamp) : base(KeysCommandNames.PExpireAt)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(millisecondsTimestamp);
        }
    }
}
