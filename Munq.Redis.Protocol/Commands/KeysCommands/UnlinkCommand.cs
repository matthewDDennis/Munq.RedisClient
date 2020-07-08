using System;
using System.Collections.Generic;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/unlink
    public class UnlinkCommand : RedisCommand
    {
        public UnlinkCommand(params string[] keys) : base(KeysCommandNames.Unlink)
        {
            if ((keys == null) || (keys.Length == 0))
            {
                throw new ArgumentException($"{nameof(keys)} is null or empty.", nameof(keys));
            }

            AddArguments(keys);
        }

        public UnlinkCommand(IEnumerable<string> keys) : base(KeysCommandNames.Unlink)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys), $"{nameof(keys)} is null.");
            }

            AddArguments(keys);
        }
    }
}
