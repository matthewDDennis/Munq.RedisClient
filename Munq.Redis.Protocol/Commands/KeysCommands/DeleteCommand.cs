using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/del
    public class DeleteCommand : RedisCommand
    {
        public DeleteCommand(params string[] keys) : base(KeysCommandNames.Delete)
        {
            if ((keys == null) || (keys.Length == 0))
            {
                throw new ArgumentException($"{nameof(keys)} is null or empty.", nameof(keys));
            }

            AddArguments(keys);
        }
    }
}
