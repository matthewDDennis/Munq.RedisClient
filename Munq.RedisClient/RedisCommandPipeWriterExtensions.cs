using System;
using System.Buffers;
using System.Buffers.Text;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;
using System.Text.Encodings;
using System.Threading;
using System.Threading.Tasks;

namespace Munq.RedisClient
{
    public static class RedisCommandPipeWriterExtensions
    {
        public static ValueTask<FlushResult> Write(this PipeWriter _writer, IRedisCommand command, CancellationToken token = default)
        {
            WriteArraySize((command.Parameters?.Length ?? 0) + 1);
            WriteBulkString(command.Name);
            if ((command.Parameters?.Length ?? 0) > 0)
                foreach(var parameter in command.Parameters)
                {
                    if (parameter == null)
                    {
                        _writer.Write(RedisConstants.NullBulkString);
                    }
                    else if (parameter is byte[] byteArray)
                    {
                        WriteBulkString(byteArray);
                    }
                    else if (parameter is bool boolValue)
                    {
                        _writer.Write(boolValue ? RedisConstants.TrueBulkString
                                                : RedisConstants.FalseBulkString);
                    }
                    else
                    {
                        // TODO: Use Utf8Encoder into span extensions to do this without creating string.
                        WriteBulkString(UTF8Encoding.UTF8.GetBytes(parameter.ToString()));
                    }
                }

            return _writer.FlushAsync(token);

            void WriteArraySize(int numElements)
            {
                _writer.Write(RedisConstants.ArrayStart);
                WriteInt(numElements);
                WriteCrLf();
            }

            void WriteBulkString(byte[] bytes)
            {
                if (bytes == null)
                {
                    _writer.Write(RedisConstants.NullBulkString);
                    return;
                }

                _writer.Write(RedisConstants.BulkStringStart);
                WriteInt(bytes.Length);
                WriteCrLf();
                _writer.Write(bytes);
                WriteCrLf();
            }

            void WriteInt(int number)
            {
                var span = _writer.GetSpan();
                int nWritten;

                Utf8Formatter.TryFormat(number, span, out nWritten);
                _writer.Advance(nWritten);
            }

            void WriteCrLf()
            {
                _writer.Write(RedisConstants.CrLf);
            }
        }
    }
}
