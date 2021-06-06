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
    public class SocketReciever : IReciever<SocketContext>
    {
        public SocketContext Context { get; private set; }

        public SocketReciever(SocketContext context)
        {
            Context = context;
        }

        IToken IReciever<SocketContext>.Receive()
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
