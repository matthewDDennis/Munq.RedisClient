# Creating a Redis Client using the .NET System.IO.Pipelines and Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets Libraries

This article is the first in a series about creating an Asynchronous client for the Redis server that is low allocation, and hence GC pressure, with minimal data copying.  This is done using the techniques used to make Kestrel one of the top ten web servers in terms of raw requests per second as recorded in [Round 13 of the TechEmpower Plain Text](https://www.techempower.com/benchmarks/#section=data-r13&hw=ph&test=plaintext) performance tests.

## Background

Sometime ago, I started writing an Async, .NET Core Redis Client.  At the time, none of the Redis Clients supported .NET Core, and I wanted to write an article on how to implement a client for a simple protocol.  

Unfortunately, the changes from VS2015 RC1 and RC2 showed that the platform was going to be unstable for sometime, and while I had a fairly complete implementation,  I put it on the shelf until things in the .NET and Visual Studio world became more stable.

With the upcoming release of VS2019, .NET 3.0 and the stabilization of the CLI, NetStandard, and tooling I think it is time to revisit this project.  One thing that has peaked my interest in the .NET Core has been how much the performance has improved, particularly around the Kestrel web server performance.

The .NET Core team, and in particular David Fowler, have taken what they learned improving Kestrel, and created a set of libraries that allow for the processing streams of data in a manner that has little or no memory allocations, and minimal data copying.  This is done by reversing the existing Stream paradigm so that instead of pushing and pulling data buffers into and out of streams, the data buffers are managed by the low level APIs and pushed up to the application.  These use highly efficient memory buffer pools and structures to achieve performance that has made Kestrel one of the fastest web servers available. 

That being said, it appears that [Kestrel](https://github.com/aspnet/AspNetCore/tree/master/src/Servers/Kestrel) now use the [System.IO.Pipelines](https://www.nuget.org/packages/System.IO.Pipelines/4.6.0-preview9.19421.4) NuGet package, and it also being used in [SignalR](https://github.com/aspnet/AspNetCore/tree/master/src/signalr).  As part of the Kestrel project, a number of low level Pipelines based libraries were created to low allocation, high performance Network IO to replace the Stream based IO.  There is an implementation for Socket based IO.  This can be found on Nuget.org at [Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets](https://www.nuget.org/packages/Microsoft.AspNetCore.Server.Kestrel.Transport.Sockets/).

## Introduction
Several years ago, here at Code Project, we took a look at the performance of our web page response time, and found it severely lacking. On each request, we were doing database requests for commonly requested data, and performing complex and expensive sanitization and formatting of content.

We embarked on a project to use Caching of various kinds of information and view models to improve the performance of the site.  This caching needed to be distributed so that all the servers in our web farm would stay consistent and current with the latest data.  After evaluating several options, we decided on [Redis](http://redis.io) due to its speed, cost, wide adoption, great reviews, and the power of its data structures and API.

The resulting performance improvement exceeded our expectations, and pages that were taking seconds, and even tens of seconds, were being returned in less a second, usually less than 500 mS, and greatly reduced the CPU load on our SQL Server.  Further performance improvements have been achieved by adding background event processing and the optimization of some expensive and heavily use algorithms, but I doubt that anything we can do will generate the improvements we obtained by using Redis.

Our current implementation use the ServiceStack Redis Client V3.  We also have an implementation using the StackExchange.Redis client, but am having a few issues with this as well. I have had to look into the code to resolve a number of issues, and as any programmer would, decided I can do it better, or at least differently.  This is mainly due to improvements in the C# language, such as Extension Methods.  This allow me to create a small client that just sends and receives stuff to and from the Redis Server. The actual commands are implemented using Extension Methods.  This eliminates the huge classes in the Service Stack implementation and some code duplication in the StackExchange implementation allowing for greater Single Responsibility of each class.

There are a number of great ideas in both of these libraries, such a ConnectionMultiplexer in the StackExchange client which allows a single socket to be shared instead of having to create a new socket connection each time one is needed access the Redis server.  Something along this line will be implemented later in this article series.

The goals of this implementation are

- Simplicity
- Performance
- Efficiency
- Robustness
- Complete Unit Testing

## Redis Protocol
Clients communicate with the Redis Server using the Redis Serialization Protocol(RESP) as detailed in [The Redis Protocol Specification](https://redis.io/topics/protocol). As the specification states:

>Redis clients communicate with the Redis server using a protocol called **RESP**. (Redis Serialization Protocol). While the protocol was designed specifically for Redis, it can be used for other client-server software projects.
> 
>RESP is a compromise between the following things:
> - Simple to implement.
> - Fast to parse.
> - Human readable.
>
>RESP can serialize different data types like integers, strings, arrays. There is also a specific type for errors. Requests are sent from the client to the Redis server as arrays of strings representing the arguments of the command to execute. Redis replies with a command specific data type.
>
>RESP is binary-safe and does not require processing of bulk data transferred from one process to another, because it uses prefixed-length to transfer bulk data.

Rather than go into detail about the protocol, I'll leave it to the reader to reference the specification if you need to clarify anything about what I am doing.  It's small, simple, and easy to understand.  I'll explain the specific protocol details when I explain the code that uses them.

## Software Design
The magic of the Pipelines based Socket Transport is a which exposes the PipeReaders and PipeWriters for a pair of Pipes.  One Pipe, the OutputPipe, transfers data from the Application to the Transport while the other, the InputPipe, transfers data from the Transport to the Application.

The Connection exposes a IDuplexPipe, Application, which has an Input PipeReader and an Output PipeWriter.  The Input is set to the InputPipe.Reader while the Output is set to the OutputPipe.Writer.  The connection has two tasks, one that reads data from the Socket and writes it to the InputPipe, and a second task the reads data from the OutputPipe and writes it to the Socket.

The Pipes use a collection of memory blocks to supply and reuse buffers for storing data.  This is not like the Streams paradigm where the user is responsible for allocating and managing data buffers used to read and write to the Stream.  The result is the Pipeline transport requires little or no buffer allocation and Garbage Collection to read and write from the Socket.  In fact, in most cases there is little need to copy data from one buffer to another until such copying is required to deserialize some object from the received data.

INSERT A DIAGRAM OF TRANSPORT PIPES HERE

This means that our Redis Protocol handler needs to do two things:
* serialize Redis Commands to bytes that are written to a PipeWriter
* read bytes from a PipeWReader and deserialize them into Redis Responses

Because of this, it is simple to create Test Transport Layer from two Pipes.  The code under test connects to the Application side, and the test reads and writes to Transport side, allowing the expected functionality to be tested without actually needing to setup a Redis instance for Unit Testing.

Of course, at some point actually communicating with a real Redis server will be required to validate the assumptions of the Unit Test.  I will use a Redis Docker Container for this purpose.