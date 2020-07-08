using System;
using System.Collections.Generic;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/touch
    public class TouchCommand : RedisCommand
    {
        public TouchCommand(params string[] keys) : base(KeysCommandNames.Touch)
        {
            if ((keys == null) || (keys.Length == 0))
            {
                throw new ArgumentException($"{nameof(keys)} is null or empty.", nameof(keys));
            }

            AddArguments(keys);
        }

        public TouchCommand(IEnumerable<string> keys) : base(KeysCommandNames.Touch)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys), $"{nameof(keys)} is null.");
            }

            AddArguments(keys);
        }
    }
}
