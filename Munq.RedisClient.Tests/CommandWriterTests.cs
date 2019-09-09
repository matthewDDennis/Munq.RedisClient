using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Buffers;
using System.Collections.Generic;
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

        [TestMethod]
        public async Task CanWriteCommandWithNoParameters()
        {
            const string commandName = "NoParamsCommand";
            RedisCommand redisCommand = new RedisCommand(commandName);
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

        [TestMethod]
        public async Task CanWriteCommandWithBoolTrueParameter()
        {
            const string commandName = "BoolParamsCommand";
            RedisCommand redisCommand = new RedisCommand(commandName, true);
            byte[] expected = Encoding.UTF8.GetBytes(
                $"*2\r\n${redisCommand.Name.Length}\r\n{commandName}\r\n$1\r\n1\r\n");

            await _handler.WriteCommand(redisCommand);
            PipeReader output = _transport.Transport.Input;
            ReadResult readResult = await output.ReadAsync();
            var buffer = readResult.Buffer;

            var span = buffer.Slice(0);
            CollectionAssert.AreEqual(expected, span.ToArray());

            output.AdvanceTo(buffer.End);
        }

        [TestMethod]
        public async Task CanWriteCommandWithBoolFalseParameter()
        {
            const string commandName = "BoolParamsCommand";
            RedisCommand redisCommand = new RedisCommand(commandName, false);
            byte[] expected = Encoding.UTF8.GetBytes(
                $"*2\r\n${redisCommand.Name.Length}\r\n{commandName}\r\n$1\r\n0\r\n");

            await _handler.WriteCommand(redisCommand);
            PipeReader output = _transport.Transport.Input;
            ReadResult readResult = await output.ReadAsync();
            var buffer = readResult.Buffer;

            var span = buffer.Slice(0);
            CollectionAssert.AreEqual(expected, span.ToArray());

            output.AdvanceTo(buffer.End);
        }

        [TestMethod]
        public async Task CanWriteCommandWithByteParameter()
        {
            await TestOneParameterCommand("ByteParamsCommand", (byte)3);
        }

        [TestMethod]
        public async Task CanWriteCommandWithDateTimeParameter()
        {
            await TestOneParameterCommand("DateTimeParamsCommand", DateTime.Now);
        }

        [TestMethod]
        public async Task CanWriteCommandWithDateTimeOffsetParameter()
        {
            await TestOneParameterCommand("DateTimeOffestParamsCommand", DateTimeOffset.Now);
        }

        [TestMethod]
        public async Task CanWriteCommandWithDecimalTimeParameter()
        {
            await TestOneParameterCommand("DecimalParamsCommand", 123.45m);
        }

        [TestMethod]
        public async Task CanWriteCommandWithDoubleParameter()
        {
            await TestOneParameterCommand("DoubleParamsCommand", 567.89);
        }

        [TestMethod]
        public async Task CanWriteCommandWithGuidParameter()
        {
            await TestOneParameterCommand("GuidParamsCommand", Guid.NewGuid());
        }

        [TestMethod]
        public async Task CanWriteCommandWithInt16Parameter()
        {
            await TestOneParameterCommand("Int16ParamsCommand", (Int16)7890);
        }

        [TestMethod]
        public async Task CanWriteCommandWithInt32Parameter()
        {
            await TestOneParameterCommand("Int32ParamsCommand", (Int32)99234);
        }

        [TestMethod]
        public async Task CanWriteCommandWithInt64Parameter()
        {
            await TestOneParameterCommand("Int64ParamsCommand", (Int64)123_456_789);
        }

        [TestMethod]
        public async Task CanWriteCommandWithSByteParameter()
        {
            await TestOneParameterCommand("SByteParamsCommand", (SByte)123);
        }

        [TestMethod]
        public async Task CanWriteCommandWithSingleParameter()
        {
            await TestOneParameterCommand("SingleParamsCommand", (Single)(-78.97));
        }

        [TestMethod]
        public async Task CanWriteCommandWithTimeSpanParameter()
        {
            await TestOneParameterCommand("TimeSpanParamsCommand", TimeSpan.FromMinutes(23));
        }

        [TestMethod]
        public async Task CanWriteCommandWithUInt16Parameter()
        {
            await TestOneParameterCommand("UInt16ParamsCommand", (UInt16)7890);
        }

        [TestMethod]
        public async Task CanWriteCommandWithUInt32Parameter()
        {
            await TestOneParameterCommand("UInt32ParamsCommand", (UInt32)99234);
        }

        [TestMethod]
        public async Task CanWriteCommandWithUInt64Parameter()
        {
            await TestOneParameterCommand("UInt64ParamsCommand", (UInt64)123_456_789);
        }

        [TestMethod]
        public async Task CanWriteCommandWithByteArrayParameter()
        {
            await TestOneByteArrayParameterCommand("ByteArryParamsCommand", new byte[] { 1, 1, 2, 3, 5, 8, 13 });
        }

        [TestMethod]
        public async Task CanWriteCommandMultipleParameters()
        {
            await TestParameterCommand("HSet", "Key1", 147);
        }

        private async Task TestOneParameterCommand(string commandName, object value)
        {
            var valueString = value.ToString();
            RedisCommand redisCommand = new RedisCommand(commandName, value);
            byte[] expected = Encoding.UTF8.GetBytes(
                $"*2\r\n${redisCommand.Name.Length}\r\n{commandName}\r\n${valueString.Length}\r\n{valueString}\r\n");

            await _handler.WriteCommand(redisCommand);
            PipeReader output = _transport.Transport.Input;
            ReadResult readResult = await output.ReadAsync();
            var buffer = readResult.Buffer;

            var span = buffer.Slice(0);
            CollectionAssert.AreEqual(expected, span.ToArray());

            output.AdvanceTo(buffer.End);
        }

        private async Task TestParameterCommand(string commandName, params object[] values)
        {
            RedisCommand redisCommand = new RedisCommand(commandName, values);
            var expectedString = $"*{values.Length + 1}\r\n${redisCommand.Name.Length}\r\n{commandName}\r\n";
            foreach(var value in values)
            {
                var valueString = value.ToString();
                expectedString += $"${valueString.Length}\r\n{valueString}\r\n";
            }
            byte[] expected = Encoding.UTF8.GetBytes(expectedString);

            await _handler.WriteCommand(redisCommand);
            PipeReader output = _transport.Transport.Input;
            ReadResult readResult = await output.ReadAsync();
            var buffer = readResult.Buffer;

            var span = buffer.Slice(0);
            CollectionAssert.AreEqual(expected, span.ToArray());

            output.AdvanceTo(buffer.End);
        }

        private async Task TestOneByteArrayParameterCommand(string commandName, byte[] value)
        {
            RedisCommand redisCommand = new RedisCommand(commandName, value);
            byte[] expected1 = Encoding.UTF8.GetBytes(
                $"*2\r\n${redisCommand.Name.Length}\r\n{commandName}\r\n${value.Length}\r\n");
            var byteList = new List<byte>(expected1);
            byteList.AddRange(value);
            byteList.AddRange(new byte[] { (byte)'\r', (byte)'\n' });
            byte[] expected = byteList.ToArray();

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
