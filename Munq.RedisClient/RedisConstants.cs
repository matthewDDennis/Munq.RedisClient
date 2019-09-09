using System;
using System.Collections.Generic;
using System.Text;

namespace Munq.RedisClient
{
    public static class RedisConstants
    {
        public static readonly byte[] ArrayStart        = new byte[] { (byte)'*' };
        public static readonly byte[] BulkStringStart   = new byte[] { (byte)'$' };
        public static readonly byte[] SimpleStringStart = new byte[] { (byte)'+' };
        public static readonly byte[] ErrorStart        = new byte[] { (byte)'-' };
        public static readonly byte[] IntegerStart      = new byte[] { (byte)':' };
        public static readonly byte[] CrLf              = new byte[] { (byte)'\r', (byte)'\n' };
        public static readonly byte[] TrueBulkString = new byte[]
        {
            (byte)'$', (byte)'1', (byte)'\r', (byte)'\n', (byte)'1', (byte)'\r', (byte)'\n'
        };
        public static readonly byte[] FalseBulkString = new byte[]
        {
            (byte)'$', (byte)'1', (byte)'\r', (byte)'\n', (byte)'0', (byte)'\r', (byte)'\n'
        };
        public static readonly byte[] NullBulkString    = new byte[]
        {
            (byte)'$', (byte)'-', (byte)'1', (byte)'\r', (byte)'\n'
        };
    }
}
