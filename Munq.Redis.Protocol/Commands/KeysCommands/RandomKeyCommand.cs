using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/randomkey
    public class RandomKeyCommand : RedisCommand
    {
        public RandomKeyCommand() : base(KeysCommandNames.RandomKey)
        { }
    }
}
