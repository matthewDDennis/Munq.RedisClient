using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    public enum KeyType
    {
        String,
        List,
        Set,
        Zset,
        Hash,
        Stream
    }

    // https://redis.io/commands/scan
    public class ScanCommand : RedisCommand
    {
        public ScanCommand(long cursor, string match, int? count, KeyType? keyType)
            : base(KeysCommandNames.Scan)
        {
            AddArgument(cursor);
            if (!string.IsNullOrWhiteSpace(match))
            {
                AddArgument("MATCH");
                AddArgument(match);
            }
            if (count.HasValue)
            {
                AddArgument("COUNT");
                AddArgument(count.Value);
            }
            if (keyType.HasValue)
            {
                AddArgument("TYPE");
                AddArgument(keyType.ToString());
            }
        }
    }
}
