using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/rename
    public class RenameCommand : RedisCommand
    {
        public RenameCommand(string key, string newKey) : base(KeysCommandNames.Rename)
        {
            if (string.IsNullOrEmpty(newKey))
            {
                throw new ArgumentException($"{nameof(newKey)} is null or empty.", nameof(newKey));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(newKey);
        }
    }
}
