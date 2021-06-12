using AsyncDesignPattern.Common.Enum;
using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Common.Enum;
using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
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
            Listener.Listen(BACK_LOG);
        }

        public SocketToken Receive()
        {
            var buffer = GetNewBuffer();

            Handler = Listener.Accept();
            var bytesRec = Handler.Receive(buffer);
            var data1 = Encoding.UTF8.GetString(buffer, 0, bytesRec);
            var msg = Encoding.UTF8.GetBytes(data1);

            Handler.Send(msg);

            return (SocketToken)JsonSerializer.Deserialize(msg, new SocketToken().GetType());
        }

        public async Task<SocketToken> ReceiveAsync(EventHandler<SocketAsyncEventArgs> callBack = null)
        {
            callBack ??= DefaultCallBack;
            EventArgs.Completed += callBack;

            await Task.Run(() =>
            {
                Listener.AcceptAsync(EventArgs);
                Listener.SendAsync(new SocketAsyncEventArgs() { 
                    UserToken = new SocketToken() 
                    { 
                        Id = ((Guid)((SocketToken)EventArgs.UserToken).Id),
                        DesingPatternType = ((DesingPatternType)((SocketToken)EventArgs.UserToken).DesingPatternType),
                        StatusType = StatusCode.Success,
                        Body = "response"
                    }
                });
            });

            return (SocketToken)EventArgs.UserToken;
        }

        public async Task<SocketToken> ReceiveAsync()
        {
            EventArgs.Completed += DefaultCallBack;

            await Task.Run(() =>
            {
                Listener.AcceptAsync(EventArgs);
                Listener.SendAsync(new SocketAsyncEventArgs()
                {
                    UserToken = new SocketToken()
                    {
                        Id = ((Guid)((SocketToken)EventArgs.UserToken).Id),
                        DesingPatternType = ((DesingPatternType)((SocketToken)EventArgs.UserToken).DesingPatternType),
                        StatusType = StatusCode.Success,
                        Body = "response"
                    }
                });
            });

            return (SocketToken)EventArgs.UserToken;
        }


        private SocketAsyncEventArgs EventArgs { get; set; } = new SocketAsyncEventArgs();

        private EventHandler<SocketAsyncEventArgs> DefaultCallBack = (object o, SocketAsyncEventArgs e) =>
        {
            Console.WriteLine(e.UserToken);
        };

        private byte[] GetNewBuffer() => new byte[1024];
    }
}
