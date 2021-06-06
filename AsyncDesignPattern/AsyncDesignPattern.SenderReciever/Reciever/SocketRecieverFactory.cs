using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Reciever
{
    public static class SocketRecieverFactory
    {
        public static SocketReciever Create(SocketContext context) => new SocketReciever(context);
    }
}
