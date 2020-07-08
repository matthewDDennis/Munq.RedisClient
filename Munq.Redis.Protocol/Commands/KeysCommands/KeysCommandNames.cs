using System;
using System.Collections.Generic;
using System.Text;

namespace Munq.Redis.Protocol.Commands
{
    public static class KeysCommandNames
    {
        public static readonly byte[] Delete      = Encoding.UTF8.GetBytes("DEL");
        public static readonly byte[] Dump        = Encoding.UTF8.GetBytes("DUMP");
        public static readonly byte[] Exists      = Encoding.UTF8.GetBytes("EXISTS");
        public static readonly byte[] Expire      = Encoding.UTF8.GetBytes("EXPIRE");
        public static readonly byte[] ExpireAt    = Encoding.UTF8.GetBytes("EXPIREAT");
        public static readonly byte[] Keys        = Encoding.UTF8.GetBytes("KEYS");
        public static readonly byte[] Migrate     = Encoding.UTF8.GetBytes("MIGRATE");
        public static readonly byte[] Move        = Encoding.UTF8.GetBytes("MOVE");
        public static readonly byte[] Object      = Encoding.UTF8.GetBytes("OBJECT");
        public static readonly byte[] Persist     = Encoding.UTF8.GetBytes("PERSIST");
        public static readonly byte[] PExpire     = Encoding.UTF8.GetBytes("PEXPIRE");
        public static readonly byte[] PExpireAt   = Encoding.UTF8.GetBytes("PEXPIREAT");
        public static readonly byte[] PTTL        = Encoding.UTF8.GetBytes("PTTL");
        public static readonly byte[] RandomKey   = Encoding.UTF8.GetBytes("RANDOMKEY");
        public static readonly byte[] Rename      = Encoding.UTF8.GetBytes("RENAME");
        public static readonly byte[] RenameNX    = Encoding.UTF8.GetBytes("RENAMENX");
        public static readonly byte[] Restore     = Encoding.UTF8.GetBytes("RESTORE");
        public static readonly byte[] Sort        = Encoding.UTF8.GetBytes("SORT");
        public static readonly byte[] Touch       = Encoding.UTF8.GetBytes("TOUCH");
        public static readonly byte[] TTL         = Encoding.UTF8.GetBytes("TTL");
        public static readonly byte[] Type        = Encoding.UTF8.GetBytes("TYPE");
        public static readonly byte[] Unlink      = Encoding.UTF8.GetBytes("UNLINK");
        public static readonly byte[] Wait        = Encoding.UTF8.GetBytes("WAIT");
        public static readonly byte[] Scan        = Encoding.UTF8.GetBytes("SCAN");
                                                  
        // Object Subcommands
        public static readonly byte[] RefCount    = Encoding.UTF8.GetBytes("REFCOUNT");
        public static readonly byte[] KeyEncoding = Encoding.UTF8.GetBytes("ENCODING");
        public static readonly byte[] IdleTime    = Encoding.UTF8.GetBytes("IDLETIME");
        public static readonly byte[] Freq        = Encoding.UTF8.GetBytes("FREQ");
        public static readonly byte[] Help        = Encoding.UTF8.GetBytes("HELP");

    }
}
