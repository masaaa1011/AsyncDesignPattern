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
        public AddressFamily AddressFamily { get; private set; }
        public SocketType SocketType { get; private set; }
        public ProtocolType ProtocolType { get; private set; }
        public IPEndPoint IPEndPoint { get; private set; }
        internal SocketContext(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, IPEndPoint iPEndPoint)
        {
            AddressFamily = addressFamily;
            SocketType = socketType;
            ProtocolType = protocolType;
            IPEndPoint = iPEndPoint;
        }
    }
}
