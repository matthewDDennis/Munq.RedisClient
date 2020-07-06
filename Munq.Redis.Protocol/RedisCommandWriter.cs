using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;

using Bedrock.Framework.Protocols;

namespace Munq.Redis.Protocol
{
    public class RedisCommandWriter : IMessageWriter<IRedisCommand>
    {
        /// <summary>
        /// Write the command to the output.
        /// </summary>
        /// <param name="command">The command to write.</param>
        /// <param name="output">The IBufferWriter to write the command to.</param>
        public void WriteMessage(IRedisCommand command, IBufferWriter<byte> output)
        {
            command.Write(output);
        }

    }
}
