using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    public class ObjectIdleTimeCommand : RedisCommand
    {
        public ObjectIdleTimeCommand(string key)
            : base(KeysCommandNames.Object, KeysCommandNames.IdleTime)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }
}
