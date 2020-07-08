using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/restore
    public class RestoreCommand : RedisCommand
    {
        public RestoreCommand(string key, long ttl, byte[] value, bool replace = false,
                                bool absttl = false, long? idleTime = null, long? freq = null)
            : base(KeysCommandNames.Restore)
        {
            if ((value == null) || (value.Length == 0))
            {
                throw new ArgumentException($"{nameof(value)} is null or empty.", nameof(value));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(ttl);
            AddArgument(value);
            if (replace)
                AddArgument("REPLACE");
            if (absttl)
                AddArgument("ABSTTL");
            if (idleTime.HasValue)
            {
                AddArgument("IDLETIME");
                AddArgument(idleTime.Value);
            }
            if (freq.HasValue)
            {
                AddArgument("FREQ");
                AddArgument(freq.Value);
            }
        }
    }
}
