using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Reciever
{
    public class SocketReciever : IReciever<SocketContext, SocketToken>
    {
        internal Socket Listener { get; set; }
        private Socket Handler { get; set; }
        public SocketContext Context { get; private set; }

        private const int BACK_LOG = 10;

        public SocketReciever(SocketContext context)
        {
            Context = context;
        }

        public SocketToken Receive(SocketToken token)
        {
            Listener.Listen(BACK_LOG);
            Handler = Listener.Accept();
            throw new NotImplementedException();
        }

        public async Task<SocketToken> ReceiveAsync(SocketToken token)
        {
            throw new NotImplementedException();
        }
        private SocketAsyncEventArgs EventArgs { get; set; } = new SocketAsyncEventArgs();

        private EventHandler<SocketAsyncEventArgs> DefaultCallBack = (object o, SocketAsyncEventArgs e) =>
        {
            Console.WriteLine(e.UserToken);
        };
    }
}
