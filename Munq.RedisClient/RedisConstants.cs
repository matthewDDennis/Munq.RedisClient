using System;
using System.Collections.Generic;
using System.Text;

namespace Munq.RedisClient
{
    public static class RedisConstants
    {
        public static readonly byte[] ArrayStart      = new byte[] { (byte)'*' };
        public static readonly byte[] BulkStringStart = new byte[] { (byte)'$' };
        public static readonly byte[] CrLf            = new byte[] { (byte)'\r', (byte)'\n' };
        public static readonly byte[] NullBulkString  = new byte[]
        {
            (byte)'$', (byte)'-', (byte)'1', (byte)'\r', (byte)'\n'
        };
    }
}
