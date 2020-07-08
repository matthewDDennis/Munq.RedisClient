using System;
using System.Collections.Generic;
using System.Text;

namespace Munq.Redis.Protocol.Commands
{
    public static class ConnectionCommandNames
    {
        public static readonly byte[] Auth      = Encoding.UTF8.GetBytes("AUTH");
        public static readonly byte[] Client    = Encoding.UTF8.GetBytes("Client");
        public static readonly byte[] Echo      = Encoding.UTF8.GetBytes("ECHO");
        public static readonly byte[] Hello     = Encoding.UTF8.GetBytes("HELLO");
        public static readonly byte[] Ping      = Encoding.UTF8.GetBytes("PING");
        public static readonly byte[] Quit      = Encoding.UTF8.GetBytes("QUIT");
        public static readonly byte[] Select    = Encoding.UTF8.GetBytes("SELECT");

        // -- Client subcommands
        public static readonly byte[] Caching  = Encoding.UTF8.GetBytes("CACHING");
        public static readonly byte[] GetName  = Encoding.UTF8.GetBytes("GETNAME");
        public static readonly byte[] GetRedir = Encoding.UTF8.GetBytes("GETREDIR");
        public static readonly byte[] ID       = Encoding.UTF8.GetBytes("ID");
        public static readonly byte[] Kill     = Encoding.UTF8.GetBytes("KILL");
        public static readonly byte[] List     = Encoding.UTF8.GetBytes("LIST");
        public static readonly byte[] Pause    = Encoding.UTF8.GetBytes("PAUSE");
        public static readonly byte[] Reply    = Encoding.UTF8.GetBytes("REPLY");
        public static readonly byte[] SetName  = Encoding.UTF8.GetBytes("SETNAME");
        public static readonly byte[] Tracking = Encoding.UTF8.GetBytes("TRACKING");
        public static readonly byte[] Unblock  = Encoding.UTF8.GetBytes("UNBLOCK");
    }
}
