using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    public class ObjectEncodingCommand : RedisCommand
    {
        public ObjectEncodingCommand(string key)
            : base(KeysCommandNames.Object, KeysCommandNames.KeyEncoding)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }
}
