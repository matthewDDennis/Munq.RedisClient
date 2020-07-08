using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/object
    public class ObjectRefCountCommand : RedisCommand
    {
        public ObjectRefCountCommand(string key)
            : base(KeysCommandNames.Object, KeysCommandNames.RefCount)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }
}
