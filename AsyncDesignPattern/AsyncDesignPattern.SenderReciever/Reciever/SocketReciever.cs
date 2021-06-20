using AsyncDesignPattern.Common.Enum;
using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Common.Enum;
using AsyncDesignPattern.SenderReciever.Common.State;
using AsyncDesignPattern.SenderReciever.Context;
using AsyncDesignPattern.TaskFamily.Controller;
using AsyncDesignPattern.TaskFamily.TaskHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Reciever
{
    public class SocketReciever : IReciever<SocketContext, SocketToken>
    {
        // Thread signal.  
        public static ManualResetEvent allDone = new ManualResetEvent(false);
        internal Socket Listener { get; set; }
        public SocketContext Context { get; internal set; }
        public SocketToken Token { get; private set; }
        private static ITaskHandler _handler { get; set; }

        private const int BACK_LOG = 150;
        public SocketReciever() { }
        public SocketReciever(SocketContext context, ITaskHandler handler)
        {
            _handler = handler;
            Context = context;
            Listener = new Socket(context.AddressFamily, context.SocketType, context.ProtocolType) { SendTimeout = context.SendTimeout, ReceiveTimeout = context.RecieveTimeout };
            Listener.Bind(context.IPEndPoint);

            Listener.Listen(BACK_LOG);
        }

        public void AddHandler(ITaskHandler handler)
        {
            _handler = handler;
        }

        public void UseContext(SocketContext context)
        {
            Context = context;
            Listener = new Socket(context.AddressFamily, context.SocketType, context.ProtocolType) { SendTimeout = context.SendTimeout, ReceiveTimeout = context.RecieveTimeout };
            Listener.Bind(context.IPEndPoint);

            Listener.Listen(BACK_LOG);
        }

        public void ReceiveAsync()
        {
            allDone.Reset();

            Console.WriteLine("Waiting for a connection...");
            Listener.BeginAccept(
                new AsyncCallback(AcceptCallback),
                Listener);

            allDone.WaitOne();
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            allDone.Set();

            Socket listener = (Socket)ar.AsyncState;
            Socket handler = listener.EndAccept(ar);

            SocketState state = new SocketState();
            state.workSocket = handler;
            handler.BeginReceive(state.buffer, 0, SocketState.BufferSize, 0,
                new AsyncCallback(ReadCallback), state);
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            String content = String.Empty;

            SocketState state = (SocketState)ar.AsyncState;
            Socket handler = state.workSocket;

            int bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                state.sb.Append(Encoding.ASCII.GetString(
                    state.buffer, 0, bytesRead));

                content = state.sb.ToString();
                var token = (SocketToken)JsonSerializer.Deserialize(content, typeof(SocketToken));

                if (!string.IsNullOrEmpty(token.EOF))
                {
                    Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                        content.Length, content);

                    _handler.Handle(
                            TaskFamily.TaskFactory.TaskFactory.Create(token.DesingPatternType.Value)
                        );

                    Send(handler, content);
                }
                else
                {
                    handler.BeginReceive(state.buffer, 0, SocketState.BufferSize, 0,
                    new AsyncCallback(ReadCallback), state);
                }
            }
        }
        private static void Send(Socket handler, String data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            handler.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                Socket handler = (Socket)ar.AsyncState;

                int bytesSent = handler.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
