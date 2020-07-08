using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/persist
    public class PersistCommand : RedisCommand
    {
        public PersistCommand(string key) : base(KeysCommandNames.Persist)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }
}
