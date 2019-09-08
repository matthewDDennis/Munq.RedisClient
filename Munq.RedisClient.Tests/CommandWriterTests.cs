using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;
using System.Threading.Tasks;

namespace Munq.RedisClient.Tests
{
    [TestClass]
    public class CommandWriterTests
    {
        TestTransport _transport;
        RedisProtocolHandler _handler;
        public CommandWriterTests()
        {
            _transport = new TestTransport();
            _handler = new RedisProtocolHandler(_transport.Application);
        }

        private class NoParamsCommand : IRedisCommand
        {
            public byte[] Name { get; }

            public object[] Parameters => null;

            public NoParamsCommand(string name)
            {
                Name = Encoding.UTF8.GetBytes(name);
            }
        }

        [TestMethod]
        public async Task CanWriteSimpleCommand()
        {
            const string commandName = "NoParamsCommand";
            NoParamsCommand redisCommand = new NoParamsCommand(commandName);
            byte[] expected = Encoding.UTF8.GetBytes(
                $"*1\r\n${redisCommand.Name.Length}\r\n{commandName}\r\n");

            await _handler.WriteCommand(redisCommand);
            PipeReader output = _transport.Transport.Input;
            ReadResult readResult = await output.ReadAsync();
            var buffer = readResult.Buffer;

            var span = buffer.Slice(0);
            CollectionAssert.AreEqual(expected, span.ToArray());
            
            output.AdvanceTo(buffer.End);
        }
    }
}
