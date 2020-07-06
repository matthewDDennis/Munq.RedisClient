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
            CommandName       = commandName;
            for (int i = 0; i < args.Length; i++)
                Parameters.Add(Encoding.UTF8.GetBytes(args[i]));
        }

        public RedisCommand(byte[] commandName, byte[] subcommandName, params string[] args)
        {
            CommandName       = commandName;
            SubCommandName    = subcommandName;
            for (int i = 0; i < args.Length; i++)
                Parameters.Add(Encoding.UTF8.GetBytes(args[i]));
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

        private bool WriteCommandStart( IBufferWriter<byte> output)
        {
            Span<byte> buffer = stackalloc byte[12];
            output.Write(RedisConstants.ArrayStart);
            if (!Utf8Formatter.TryFormat(Parameters.Count + (SubCommandName == null ? 1 : 2), buffer, out int bytesWritten))
                return false;
            output.Write(buffer.Slice(0, bytesWritten));
            output.Write(RedisConstants.CrLf);

            if (!WriteBulkString(CommandName, output))
                return false;

            if (SubCommandName != null && !WriteBulkString(SubCommandName, output))
                return false;

            return true;
        }

        private static bool WriteBulkString(byte[] input, IBufferWriter<byte> output)
        {
            Span<byte> buffer = stackalloc byte[12];
            output.Write(RedisConstants.BulkStringStart);
            if (!Utf8Formatter.TryFormat(input.Length, buffer, out int bytesWritten))
                return false;
            output.Write(buffer.Slice(0, bytesWritten));
            output.Write(RedisConstants.CrLf);

            output.Write(input);
            output.Write(RedisConstants.CrLf);

            return true;
        }
    }
}
