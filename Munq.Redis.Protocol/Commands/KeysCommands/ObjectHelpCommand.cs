using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    public class ObjectHelpCommand : RedisCommand
    {
        public ObjectHelpCommand()
            : base(KeysCommandNames.Object, KeysCommandNames.Help)
        { }
    }
}
