using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    public class ObjectFreqCommand : RedisCommand
    {
        public ObjectFreqCommand(string key)
            : base(KeysCommandNames.Object, KeysCommandNames.Freq)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }
}
