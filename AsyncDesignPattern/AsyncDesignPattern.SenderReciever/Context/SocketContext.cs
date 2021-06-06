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
        internal SocketContext(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, IPEndPoint iPEndPoint)
        {
            Socket = new Socket(addressFamily, socketType, protocolType);
            Socket.Bind(iPEndPoint);
        }

        internal Socket Socket { get; set; }
    }
}
