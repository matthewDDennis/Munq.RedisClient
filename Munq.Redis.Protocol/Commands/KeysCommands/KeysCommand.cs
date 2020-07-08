using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/keys
    public class KeysCommand : RedisCommand
    {
        public KeysCommand(string pattern) : base(KeysCommandNames.Keys)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentException($"{nameof(pattern)} is null or empty.", nameof(pattern));
            }

            AddArgument(pattern);
        }
    }
}
