using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Sender
{
    public static class SocketSenderFactory
    {
        public static SocketSender Create(SocketContext context)
        {
            var socket = new SocketSender(context)
            {
                Socket = new Socket(context.AddressFamily, context.SocketType, context.ProtocolType)
            };
            socket.Socket.Bind(context.IPEndPoint);

            return socket;
        }
    }
}