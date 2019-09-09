using System;
using System.Collections.Generic;
using System.Text;

namespace Munq.RedisClient
{
    public class RedisCommand : IRedisCommand
    {
        public byte[] Name { get; }

        public object[] Parameters { get; } = null;

        public RedisCommand(string name, params object[] parameters)
        {
            Name       = Encoding.UTF8.GetBytes(name);
            Parameters = parameters;
        }
    }
}
