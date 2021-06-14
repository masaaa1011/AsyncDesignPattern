using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Context
{
    public class SocketContext : IContext
    {
        public AddressFamily AddressFamily { get; set; }
        public SocketType SocketType { get; set; }
        public ProtocolType ProtocolType { get; set; }
        public IPEndPoint IPEndPoint { get; set; }
        public int SendTimeout { get; set; } = 15;
        public int RecieveTimeout { get; set; } = 15;
        public SocketContext() { }
    }
}
