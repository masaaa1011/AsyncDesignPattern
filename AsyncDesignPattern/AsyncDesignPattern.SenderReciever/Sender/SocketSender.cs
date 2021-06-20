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

        private static ManualResetEvent connectDone = new ManualResetEvent(false);
        private static ManualResetEvent sendDone = new ManualResetEvent(false);
        private static ManualResetEvent receiveDone = new ManualResetEvent(false);
        // The response from the remote device.  
        private static String response = String.Empty;

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
        public SocketToken Send(SocketToken token)
        {
            Socket.Connect(Context.IPEndPoint);
            Socket.Send(Encoding.UTF8.GetBytes(JsonSerializer.Serialize(token)));
            var buffer = new List<byte>();
            var bufferRec = Socket.Receive(buffer.ToArray());
            var response = Encoding.UTF8.GetString(buffer.ToArray(), 0, bufferRec);

            return (SocketToken)JsonSerializer.Deserialize(response, token.GetType());
        }

        public void SendAsync(SocketToken token)
        {
            try
            {
                var client = new Socket(Context.AddressFamily, Context.SocketType, Context.ProtocolType);

                client.BeginConnect(Context.IPEndPoint,
                    new AsyncCallback(ConnectCallback), client);
                connectDone.WaitOne();

                Send(client, JsonSerializer.Serialize(token));
                sendDone.WaitOne();

                Receive(client);
                receiveDone.WaitOne();

                Console.WriteLine("Response received : {0}", response);

                //hack: socketのインスタンスをdisposeしてもイベントハンドラ内でアクセスをしてしまう。
                //undone: イベントハンドラにてdisposeされたsocketにアクセスしないように実装する必要がある
                //client.Shutdown(SocketShutdown.Both);
                //client.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private static void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket)ar.AsyncState;
                client.EndConnect(ar);
                Console.WriteLine("Socket connected to {0}",
                    client.RemoteEndPoint.ToString());
                connectDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Receive(Socket client)
        {
            try
            {
                var state = new SocketState();
                state.workSocket = client;

                client.BeginReceive(state.buffer, 0, SocketState.BufferSize, 0,
                    new AsyncCallback(ReceiveCallback), state);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var state = (SocketState)ar.AsyncState;
                var client = state.workSocket;

                int bytesRead = client.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));

                    client.BeginReceive(state.buffer, 0, SocketState.BufferSize, 0,
                        new AsyncCallback(ReceiveCallback), state);
                }
                else
                {
                    if (state.sb.Length > 1)
                    {
                        response = state.sb.ToString();
                    }
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void Send(Socket client, string data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);
            client.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), client);
        }

        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket)ar.AsyncState;
                int bytesSent = client.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to server.", bytesSent);

                sendDone.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
