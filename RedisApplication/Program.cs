using Bedrock.Framework;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.IO.Pipelines;

using System;
using System.Net;
using System.Threading.Tasks;
using Munq.Redis.Protocol;
using Munq.Redis.Protocol.Commands;

namespace RedisApplication
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var serviceProvider = new ServiceCollection().AddLogging(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Debug);
                builder.AddConsole();
            })
            .BuildServiceProvider();

            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.SetMinimumLevel(LogLevel.Error);
                builder.AddConsole();
            });

            var client = new ClientBuilder(serviceProvider)
                .UseSockets()
                .UseConnectionLogging(loggerFactory: loggerFactory)
                .Build();

            var ipAddress = IPAddress.Parse("127.0.0.1");
            var connection = await client.ConnectAsync(new IPEndPoint(ipAddress, 6379));

            Console.WriteLine($"Connected to {connection.LocalEndPoint}");

            var redis = new RedisProtocol(connection);

            //Console.WriteLine("Connected to Redis, type into the console");
            //var reads  = Console.OpenStandardInput().CopyToAsync(connection.Transport.Output);
            var writes = connection.Transport.Input.CopyToAsync(Console.OpenStandardOutput());
            await redis.SendAsync(new AuthCommand("password"));
            await redis.SendAsync(new AuthCommand("Matthew", "password"));

            await redis.SendAsync(new PingCommand());
            await redis.SendAsync(new PingCommand("Hello World!"));

            //await reads;
            await writes;


        }
    }
}
