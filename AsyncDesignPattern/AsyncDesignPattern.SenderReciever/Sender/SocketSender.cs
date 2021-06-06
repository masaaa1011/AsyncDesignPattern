using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Sender
{
    public class SocketSender : ISender<SocketContext, SocketToken>
    {
        internal Socket Socket { get; set; }

        internal SocketSender(SocketContext context)
        {
            Context = context;
        }

        public SocketContext Context { get; private set; }
        async Task<SocketToken> ISender<SocketContext, SocketToken>.SendAsync(SocketToken token)
        {
            EventArgs.Completed += DefaultCallBack;

            await Task.Run(() =>
            {
                Socket.SendAsync(EventArgs);
                Socket.ReceiveAsync(EventArgs);
            });

            return (SocketToken)EventArgs.UserToken;
        }
        public async Task<SocketToken> SendAsync(SocketToken token, EventHandler<SocketAsyncEventArgs> callback = null)
        {
            callback ??= DefaultCallBack;
            EventArgs.Completed += callback;

            await Task.Run(() =>
            {
                Socket.SendAsync(EventArgs);
                Socket.ReceiveAsync(EventArgs);
            });

            return (SocketToken)EventArgs.UserToken;
        }

        public SocketToken Send(SocketToken token)
        {
            Socket.Send(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(token)));
            var buffer = new List<byte>();
            var bufferRec = Socket.Receive(buffer.ToArray());
            var response = Encoding.UTF8.GetString(buffer.ToArray(), 0, bufferRec);

            return (SocketToken)JsonSerializer.Deserialize(response, token.GetType());
        }

        private SocketAsyncEventArgs EventArgs { get; set; } = new SocketAsyncEventArgs();

        private EventHandler<SocketAsyncEventArgs> DefaultCallBack = (object o, SocketAsyncEventArgs e) =>
        {
            Console.WriteLine(e.UserToken);
        };
    }
}
