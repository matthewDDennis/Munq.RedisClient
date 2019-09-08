using System;
using System.Collections.Generic;
using System.IO.Pipelines;
using System.Text;

namespace Munq.RedisClient.Tests
{
    public class TestTransport
    {
        public IDuplexPipe Application { get; }
        public IDuplexPipe Transport { get; }

        private class DuplexPipe : IDuplexPipe
        {
            public PipeReader Input { get; }

            public PipeWriter Output { get; }

            public DuplexPipe(PipeReader input, PipeWriter output)
            {
                Input  = input;
                Output = output;
            }
        }


        public TestTransport()
        {
            var applicationToTransportPipe = new Pipe();
            var transportToApplicationPipe = new Pipe();

            Application = new DuplexPipe(transportToApplicationPipe.Reader, 
                                         applicationToTransportPipe.Writer);

            Transport   = new DuplexPipe(applicationToTransportPipe.Reader, 
                                         transportToApplicationPipe.Writer);
        }
    }
}
