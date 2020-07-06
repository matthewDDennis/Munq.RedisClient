using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace Munq.Redis.Protocol
{
    public interface IRedisCommand
    {
        byte[] CommandName    { get; }
        byte[] SubCommandName { get; }
        List<byte[]> Parameters { get; }

        void Write(IBufferWriter<byte> output);
    }
}
