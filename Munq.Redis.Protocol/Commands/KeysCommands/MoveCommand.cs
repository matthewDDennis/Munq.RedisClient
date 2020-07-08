using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/move
    public class MoveCommand : RedisCommand
    {
        public MoveCommand(string key, int databaseNumber) : base(KeysCommandNames.Move)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(databaseNumber);
        }
    }
}
