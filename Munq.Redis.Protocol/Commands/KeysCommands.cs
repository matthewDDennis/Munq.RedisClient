using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/del
    public  class DeleteCommand : RedisCommand            
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

    // https://redis.io/commands/dump
    public class DumpCommand : RedisCommand
    {
        public DumpCommand(string key) : base(KeysCommandNames.Dump)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }

    // https://redis.io/commands/exists
    public class ExistsCommand : RedisCommand
    {
        public ExistsCommand(params string[] keys) : base(KeysCommandNames.Delete)
        {
            if ((keys == null) || (keys.Length == 0))
            {
                throw new ArgumentException($"{nameof(keys)} is null or empty.", nameof(keys));
            }

            AddArguments(keys);
        }

    }

    // https://redis.io/commands/expire
    public class ExpireCommand : RedisCommand
    {
        public ExpireCommand(string key, int seconds) : base(KeysCommandNames.Expire)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(seconds);
        }
    }

    // https://redis.io/commands/expireat
    public class ExpireAtCommand : RedisCommand
    {
        public ExpireAtCommand(string key, long timestamp) : base(KeysCommandNames.ExpireAt)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(timestamp);
        }
    }

    // https://redis.io/commands/keys
    public class KeysCommand : RedisCommand
    {
        public KeysCommand(string pattern) : base(KeysCommandNames.Keys)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentException($"{nameof(pattern)} is null or empty.", nameof(pattern));
            }

            AddArgument(pattern);
        }
    }

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

    // https://redis.io/commands/object
    public class ObjectRefCountCommand : RedisCommand
    {
        public ObjectRefCountCommand(string key)
            : base(KeysCommandNames.Object, KeysCommandNames.RefCount)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }

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

    public class ObjectHelpCommand : RedisCommand
    {
        public ObjectHelpCommand()
            : base(KeysCommandNames.Object, KeysCommandNames.Help)
        {}
    }

    // https://redis.io/commands/persist
    public class PersistCommand : RedisCommand
    {
        public PersistCommand(string key) : base(KeysCommandNames.Persist)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }

    // https://redis.io/commands/pexpire
    public class PExpireCommand : RedisCommand
    {
        public PExpireCommand(string key, long milliseconds) : base(KeysCommandNames.PExpire)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(milliseconds);
        }
    }

    // https://redis.io/commands/pexpireat
    public class PExpireAtCommand : RedisCommand
    {
        public PExpireAtCommand(string key, long millisecondsTimestamp) : base(KeysCommandNames.PExpireAt)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(millisecondsTimestamp);
        }
    }

    // https://redis.io/commands/pttl
    public class PTTLCommand : RedisCommand
    {
        public PTTLCommand(string key) : base(KeysCommandNames.PTTL)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }

    // https://redis.io/commands/randomkey
    public class RandomKeyCommand : RedisCommand
    {
        public RandomKeyCommand() : base(KeysCommandNames.RandomKey)
        {}
    }

    // https://redis.io/commands/rename
    public class RenameCommand : RedisCommand
    {
        public RenameCommand(string key, string newKey) : base(KeysCommandNames.Rename)
        {
            if (string.IsNullOrEmpty(newKey))
            {
                throw new ArgumentException($"{nameof(newKey)} is null or empty.", nameof(newKey));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(newKey);
        }
    }

    // https://redis.io/commands/renamenx
    public class RenameNxCommand : RedisCommand
    {
        public RenameNxCommand(string key, string newKey) : base(KeysCommandNames.RenameNX)
        {
            if (string.IsNullOrEmpty(newKey))
            {
                throw new ArgumentException($"{nameof(newKey)} is null or empty.", nameof(newKey));
            }

            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
            AddArgument(newKey);
        }
    }

    // https://redis.io/commands/restore
    public class RestoreCommand : RedisCommand
    {
        public RestoreCommand(string key, long ttl, byte[] value, bool replace = false,
                                bool absttl = false, long? idleTime = null, long? freq = null)
            :base(KeysCommandNames.Restore)
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

    // https://redis.io/commands/sort
    public class SortCommand : RedisCommand
    {
        public SortCommand() : base(KeysCommandNames.Sort)
        {
            throw new NotImplementedException();
        }
    }

    // https://redis.io/commands/touch
    public class TouchCommand : RedisCommand
    {
        public TouchCommand(params string[] keys) : base(KeysCommandNames.Touch)
        {
            if ((keys == null) || (keys.Length == 0))
            {
                throw new ArgumentException($"{nameof(keys)} is null or empty.", nameof(keys));
            }

            AddArguments(keys);
        }

        public TouchCommand(IEnumerable<string> keys) : base(KeysCommandNames.Touch)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys), $"{nameof(keys)} is null.");
            }

            AddArguments(keys);
        }
    }

    // https://redis.io/commands/ttl
    public class TTLCommand : RedisCommand
    {
        public TTLCommand(string key) : base(KeysCommandNames.TTL)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }

    // https://redis.io/commands/type
    public class TypeCommand : RedisCommand
    {
        public TypeCommand(string key) : base(KeysCommandNames.Type)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException($"{nameof(key)} is null or empty.", nameof(key));
            }

            AddArgument(key);
        }
    }

    // https://redis.io/commands/unlink
    public class UnlinkCommand : RedisCommand
    {
        public UnlinkCommand(params string[] keys) : base(KeysCommandNames.Unlink)
        {
            if ((keys == null) || (keys.Length == 0))
            {
                throw new ArgumentException($"{nameof(keys)} is null or empty.", nameof(keys));
            }

            AddArguments(keys);
        }

        public UnlinkCommand(IEnumerable<string> keys) : base(KeysCommandNames.Unlink)
        {
            if (keys == null)
            {
                throw new ArgumentNullException(nameof(keys), $"{nameof(keys)} is null.");
            }

            AddArguments(keys);
        }
    }

    // https://redis.io/commands/wait
    public class WaitCommand : RedisCommand
    {
        public WaitCommand(int numReplicas, long milliseconds) : base(KeysCommandNames.Wait)
        {
            AddArgument(numReplicas);
            AddArgument(milliseconds);
        }
    }
}
