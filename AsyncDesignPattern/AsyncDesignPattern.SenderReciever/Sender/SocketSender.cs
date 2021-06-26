using AsyncDesignPattern.Common.Enum;
using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Common.Enum;
using AsyncDesignPattern.SenderReciever.Common.State;
using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Sender
{
    public class SocketSender : ISender<SocketContext, SocketToken>
    {
        internal Socket Socket { get; set; }
        public SocketContext Context { get; private set; }
        public SocketToken Token { get; private set; }
        public SocketSender() { }
        internal SocketSender(SocketContext context)
        {
            Context = context;
            Socket = new Socket(context.AddressFamily, context.SocketType, context.ProtocolType) { SendTimeout = context.SendTimeout, ReceiveTimeout = context.RecieveTimeout };
        }

        public void UseContext(SocketContext context)
        {
            Context = context;
            Socket = new Socket(context.AddressFamily, context.SocketType, context.ProtocolType) { SendTimeout = context.SendTimeout, ReceiveTimeout = context.RecieveTimeout };
        }

        public void SendAsync(SocketToken token)
        {
            try
            {
                using (var client = SocketSenderSet.Create(Context))
                {
                    client.Socket.BeginConnect(Context.IPEndPoint,
                        new AsyncCallback(ConnectCallback), client);

                    client.connectDone.WaitOne();
                    Send(client, JsonSerializer.Serialize(token));
                    client.sendDone.WaitOne();

                    Receive(client);
                    client.receiveDone.WaitOne();
                    Console.WriteLine("Response received : {0}", client.Token.Payload??= "null");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void ConnectCallback(IAsyncResult ar)
        {
            var client = (SocketSenderSet)ar.AsyncState;
            try
            {
                client.Socket.EndConnect(ar);
                Console.WriteLine("Socket connected to {0}",
                    client.Socket.RemoteEndPoint.ToString());
                client.connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                client.connectDone.Set();
            }
        }

        private static void Receive(SocketSenderSet client)
        {
            try
            {
                var state = new SocketOption();
                state.workSocket = client;

                client.Socket.BeginReceive(state.buffer, 0, SocketOption.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            var state = (SocketOption)ar.AsyncState;
            var client = (SocketSenderSet)state.workSocket;
            try
            {

                int bytesRead = client.Socket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    client.Socket.BeginReceive(state.buffer, 0, SocketOption.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    if (state.sb.Length > 1)
                    {
                        client.Token = (SocketToken)JsonSerializer.Deserialize(state.sb.ToString(), typeof(SocketToken));
                    }
                    client.receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                client.receiveDone.Set();
            }
        }

        private static void Send(SocketSenderSet client, string data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.Socket.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            var client = (SocketSenderSet)ar.AsyncState;
            try
            {
                int bytesSent = client.Socket.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                client.sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                client.sendDone.Set();
            }
        }
    }
}
