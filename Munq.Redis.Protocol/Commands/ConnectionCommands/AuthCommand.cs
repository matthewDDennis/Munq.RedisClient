using System;
using System.Linq;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/auth
    public class AuthCommand : RedisCommand
    {
        public AuthCommand(string password) : base(ConnectionCommandNames.Auth, password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"{nameof(password)} is null or empty.", nameof(password));
            }
        }

        public AuthCommand(string username, string password)
            : base(ConnectionCommandNames.Auth, username, password)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentException($"{nameof(password)} is null or empty.", nameof(password));
            }

            if (string.IsNullOrEmpty(username))
            {
                throw new ArgumentException($"{nameof(username)} is null or empty.", nameof(username));
            }
        }
    }
}
