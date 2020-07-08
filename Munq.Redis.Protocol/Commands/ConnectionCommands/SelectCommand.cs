using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime;
using System.Text;

namespace Munq.Redis.Protocol.Commands
{
    // https://redis.io/commands/select
    public class SelectCommand : RedisCommand
    {
        public SelectCommand(int databaseNum) : base(ConnectionCommandNames.Select)
        {
            AddArgument(databaseNum);
        }
    }
}
