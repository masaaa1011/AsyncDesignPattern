using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Common.State;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Reciever
{
    public class SocketRecieverSet : IStateSet
    {
        public ManualResetEvent allDone = new ManualResetEvent(false);
        public Socket Socket { get; set; }
        public SocketToken Token { get; set; }
    }
}
