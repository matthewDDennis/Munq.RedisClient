using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.Unicode;

namespace Munq.Redis.Protocol.Commands
{
    public class RedisCommand : IRedisCommand
    {
        
        public byte[] CommandName { get; }

        public byte[] SubCommandName { get; }

        public List<byte[]> Parameters { get; } = new List<byte[]>();

        //public RedisCommand()
        //{
        //    Parameters = new byte[0][];
        //}

        public RedisCommand(byte[] commandName, params string[] args)
        {
            CommandName = commandName;
            AddArguments(args);
        }

        public RedisCommand(byte[] commandName, byte[] subcommandName, params string[] args)
        {
            CommandName    = commandName;
            SubCommandName = subcommandName;
            AddArguments(args);
        }

        public void Write(IBufferWriter<byte> output)
        {
            WriteCommandStart(output);
            foreach(var parameter in Parameters)
                WriteBulkString(parameter, output);
        }

        public void AddArgument(string arg)
        {
            AddArgument(Encoding.UTF8.GetBytes(arg));
        }

        public void AddArgument(byte[] arg)
        {
            Parameters.Add(arg);
        }

        public void AddArgument(int number)
        {
            Parameters.Add(NumberToBytes(number));
        }

        public void AddArgument(long number)
        {
            Parameters.Add(NumberToBytes(number));
        }

        public void AddArguments(IEnumerable<string> args)
        {
            foreach (var arg in args)
                AddArgument(arg);
        }

        public void AddArguments(IEnumerable<byte[]> args)
        {
            Parameters.AddRange(args);
        }

        private bool WriteCommandStart(IBufferWriter<byte> output)
        {
            output.Write(RedisConstants.ArrayStart);
            int numParameters = Parameters.Count + (SubCommandName == null ? 1 : 2);
            if (!WriteNumber(numParameters, output))
                return false;
            output.Write(RedisConstants.CrLf);

            if (!WriteBulkString(CommandName, output))
                return false;

            if (SubCommandName != null && !WriteBulkString(SubCommandName, output))
                return false;

            return true;
        }

        private static bool WriteBulkString(byte[] input, IBufferWriter<byte> output)
        {
            output.Write(RedisConstants.BulkStringStart);
            if (!WriteNumber(input.Length, output))
                return false;
            output.Write(RedisConstants.CrLf);

            output.Write(input);
            output.Write(RedisConstants.CrLf);

            return true;
        }

        private static bool WriteNumber(int number, IBufferWriter<byte> output)
        {
            Span<byte> buffer = stackalloc byte[12];
            if (!Utf8Formatter.TryFormat(number, buffer, out int bytesWritten))
                return false;
            output.Write(buffer.Slice(0, bytesWritten));
            return true;
        }

        private static bool WriteNumber(long number, IBufferWriter<byte> output)
        {
            Span<byte> buffer = stackalloc byte[20];
            if (!Utf8Formatter.TryFormat(number, buffer, out int bytesWritten))
                return false;
            output.Write(buffer.Slice(0, bytesWritten));
            return true;
        }

        private static byte[] NumberToBytes(long number)
        {
            Span<byte> buffer = stackalloc byte[20];

            if (!Utf8Formatter.TryFormat(number, buffer, out int bytesWritten))
                return Array.Empty<byte>();

            return buffer.Slice(0, bytesWritten).ToArray();
        }

        private static byte[] NumberToBytes(int number)
        {
            Span<byte> buffer = stackalloc byte[12];

            if (!Utf8Formatter.TryFormat(number, buffer, out int bytesWritten))
                return Array.Empty<byte>();

            return buffer.Slice(0, bytesWritten).ToArray();
        }

    }
}
