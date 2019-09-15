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
        Pipe _pipe = new Pipe();


        [TestMethod]
        public async Task CanWriteCommandWithNoParameters()
        {
            const string commandName = "NoParamsCommand";
            RedisCommand redisCommand = new RedisCommand(commandName);
            byte[] expected = Encoding.UTF8.GetBytes(
                $"*1\r\n${redisCommand.Name.Length}\r\n{commandName}\r\n");

            await _pipe.Writer.Write(redisCommand);
            ReadResult readResult = await _pipe.Reader.ReadAsync();
            var buffer = readResult.Buffer;

            var span = buffer.Slice(0);
            CollectionAssert.AreEqual(expected, span.ToArray());

            _pipe.Reader.AdvanceTo(buffer.End);
        }

        [TestMethod]
        public async Task CanWriteCommandWithBoolTrueParameter()
        {
            const string commandName = "BoolParamsCommand";
            RedisCommand redisCommand = new RedisCommand(commandName, true);
            byte[] expected = Encoding.UTF8.GetBytes(
                $"*2\r\n${redisCommand.Name.Length}\r\n{commandName}\r\n$1\r\n1\r\n");

            await _pipe.Writer.Write(redisCommand);
            ReadResult readResult = await _pipe.Reader.ReadAsync();
            var buffer = readResult.Buffer;

            var span = buffer.Slice(0);
            CollectionAssert.AreEqual(expected, span.ToArray());

            _pipe.Reader.AdvanceTo(buffer.End);
        }

        [TestMethod]
        public async Task CanWriteCommandWithBoolFalseParameter()
        {
            const string commandName = "BoolParamsCommand";
            RedisCommand redisCommand = new RedisCommand(commandName, false);
            byte[] expected = Encoding.UTF8.GetBytes(
                $"*2\r\n${redisCommand.Name.Length}\r\n{commandName}\r\n$1\r\n0\r\n");

            await _pipe.Writer.Write(redisCommand);
            ReadResult readResult = await _pipe.Reader.ReadAsync();
            var buffer = readResult.Buffer;

            var span = buffer.Slice(0);
            CollectionAssert.AreEqual(expected, span.ToArray());

            _pipe.Reader.AdvanceTo(buffer.End);
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

        private Task TestOneParameterCommand(string commandName, object value)
        {
            return TestParameterCommand(commandName, value);
        }

        private async Task TestParameterCommand(string commandName, params object[] values)
        {
            RedisCommand redisCommand = new RedisCommand(commandName, values);
            var expectedString = $"*{values.Length + 1}\r\n${redisCommand.Name.Length}\r\n{commandName}\r\n";
            var expectedBytes = new List<byte>();
            expectedBytes.AddRange(Encoding.UTF8.GetBytes(expectedString));

            foreach(var value in values)
            {
                WriteArrayElement(expectedBytes, value);
            }


            await _pipe.Writer.Write(redisCommand);
            ReadResult readResult = await _pipe.Reader.ReadAsync();
            var buffer = readResult.Buffer;

            var span = buffer.Slice(0);
            CollectionAssert.AreEqual(expectedBytes.ToArray(), span.ToArray());

            _pipe.Reader.AdvanceTo(buffer.End);
        }

        private static void WriteArrayElement(List<byte> expectedBytes, object value)
        {
            if (value is byte[] byteArray)
            {
                var valueString = $"${byteArray.Length}\r\n";
                expectedBytes.AddRange(Encoding.UTF8.GetBytes($"${byteArray.Length}\r\n"));
                expectedBytes.AddRange(byteArray);
                expectedBytes.AddRange(new byte[] { (byte)'\r', (byte)'\n' });

            }
            else
            {
                string valueString;
                if (value is bool boolValue)
                    valueString = (boolValue ? 1 : 0).ToString();
                else
                    valueString = value.ToString();
                var expectedString = $"${valueString.Length}\r\n{valueString}\r\n";
                expectedBytes.AddRange(Encoding.UTF8.GetBytes(expectedString));
            }
        }

        private Task TestOneByteArrayParameterCommand(string commandName, byte[] value)
        {
            return TestParameterCommand(commandName, value);
        }
    }
}
