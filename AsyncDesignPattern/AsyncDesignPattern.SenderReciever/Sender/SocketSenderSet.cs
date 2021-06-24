using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Common.State;
using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Sender
{
    public class SocketSenderSet : IStateSet
    {
        public static SocketSenderSet Create(SocketContext context)
            => new SocketSenderSet(context);

        private SocketSenderSet(SocketContext context)
        {
            Socket = new Socket(context.AddressFamily, context.SocketType, context.ProtocolType) { SendTimeout = context.SendTimeout, ReceiveTimeout = context.RecieveTimeout };
        }

        public Socket Socket { get; set; }

        public ManualResetEvent connectDone = new ManualResetEvent(false);
        public ManualResetEvent sendDone = new ManualResetEvent(false);
        public ManualResetEvent receiveDone = new ManualResetEvent(false);
        public SocketToken Token { get; set;}
    }
}
