using Bedrock.Framework.Protocols;

using Microsoft.AspNetCore.Connections;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Munq.Redis.Protocol
{
    public class RedisProtocol
    {
        private readonly ProtocolWriter     _writer;
        private readonly ProtocolReader     _reader;
        private readonly RedisCommandWriter _commandWriter;

        public RedisProtocol(ConnectionContext connection)
        {
            _writer        = connection.CreateWriter();
            _reader        = connection.CreateReader();
            _commandWriter = new RedisCommandWriter();
        }

        public ValueTask SendAsync(IRedisCommand message)
        {
            return _writer.WriteAsync(_commandWriter, message);
        }

        //public async Task<T> ReceiveAsync<T>() where T : IAmqpMessage
        //{
        //    var result = await _reader.ReadAsync(_formatter);
        //    _reader.Advance();
        //    return (T)result.Message;
        //}
    }
}
