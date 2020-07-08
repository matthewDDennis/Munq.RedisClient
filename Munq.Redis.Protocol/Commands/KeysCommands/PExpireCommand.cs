using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/pexpire
    public class PExpireCommand : RedisCommand
    {
        public PExpireCommand(string key, long milliseconds) : base(KeysCommandNames.PExpire)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(milliseconds);
        }
    }
}
