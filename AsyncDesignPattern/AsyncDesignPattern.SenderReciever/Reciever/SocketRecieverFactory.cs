using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Reciever
{
    public static class SocketRecieverFactory
    {
        public static SocketReciever Create(SocketContext context)
        {
            var socket = new SocketReciever(context)
            {
                Listener = new Socket(context.AddressFamily, context.SocketType, context.ProtocolType)
            };
            socket.Listener.Bind(context.IPEndPoint);

            return socket;
        }
    }
}
