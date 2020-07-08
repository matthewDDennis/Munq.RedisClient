using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/migrate
    public class MigrateCommand : RedisCommand
    {
        public MigrateCommand(string host, int port, string key, int destDbNumber, long timeout,
            bool copy, bool replace, string password, params string[] keys)
            : base(KeysCommandNames.Migrate)
        {
            if (string.IsNullOrEmpty(host))
            {
                throw new ArgumentException($"{nameof(host)} is null or empty.", nameof(host));
            }

            key = key ?? String.Empty;

            AddArgument(host);
            AddArgument(port);
            AddArgument(key);
            AddArgument(destDbNumber);
            AddArgument(timeout);
            if (copy)
                AddArgument("COPY");
            if (replace)
                AddArgument("REPLACE");
            if (!string.IsNullOrWhiteSpace(password))
            {
                AddArgument("AUTH");
                AddArgument(password);
            }
            if (keys.Any())
            {
                AddArgument("KEYS");
                AddArguments(keys);
            }
        }
    }
}
