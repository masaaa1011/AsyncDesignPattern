using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Sender
{
    public static class SocketSenderFactory
    {
        public static SocketSender Create(SocketContext context) => new SocketSender(context);
    }
}
