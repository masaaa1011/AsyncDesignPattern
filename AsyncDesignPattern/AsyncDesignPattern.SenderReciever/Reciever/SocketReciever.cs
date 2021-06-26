using AsyncDesignPattern.Common.Enum;
using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Common.Enum;
using AsyncDesignPattern.SenderReciever.Common.State;
using AsyncDesignPattern.SenderReciever.Context;
using AsyncDesignPattern.TaskFamily.Contracts;
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
        // hack: 以下のThread signalオブジェクトをstaticではない実装にした方がよい
        public static ManualResetEvent threadSignal = new ManualResetEvent(false);
        internal Socket Listener { get; set; }
        public SocketContext Context { get; private set; }
        public SocketToken Token { get; private set; }
        public static ITaskHandler Handler { get; private set; }
        private const int BACK_LOG = 150;
        
        public SocketReciever() { }
        public SocketReciever(SocketContext context, ITaskHandler handler)
        {
            Handler = handler;
            Context = context;
            Listener = new Socket(context.AddressFamily, context.SocketType, context.ProtocolType) { SendTimeout = context.SendTimeout, ReceiveTimeout = context.RecieveTimeout };
            Listener.Bind(context.IPEndPoint);

            Listener.Listen(BACK_LOG);
        }

        public ITaskHandler AddHandler(ITaskHandler handler)
        {
            Handler = handler;
            return Handler;
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
            threadSignal.Reset();

            Console.WriteLine("Waiting for a connection...");
            Listener.BeginAccept(
                new AsyncCallback(AcceptCallback),
                Listener);

            threadSignal.WaitOne();
        }

        public static void AcceptCallback(IAsyncResult ar)
        {
            threadSignal.Set();
            try
            {
                var listener = (Socket)ar.AsyncState;
                var handler = listener.EndAccept(ar);
                var option = new SocketOption();

                option.workSocket = new SocketRecieverSet() { Socket = handler };
                handler.BeginReceive(option.buffer, 0, SocketOption.BufferSize, 0,
                    new AsyncCallback(ReadCallback), option);
            }
            catch(Exception e)
            {
                threadSignal.Reset();
                throw;
            }
        }

        public static void ReadCallback(IAsyncResult ar)
        {
            try
            {
                SocketOption state = (SocketOption)ar.AsyncState;
                var handler = (SocketRecieverSet)state.workSocket;
                int bytesRead = handler.Socket.EndReceive(ar);

                if (bytesRead > 0)
                {
                    state.sb.Append(Encoding.ASCII.GetString(
                        state.buffer, 0, bytesRead));

                    var content = state.sb.ToString();
                    var token = (SocketToken)JsonSerializer.Deserialize(content, typeof(SocketToken));

                    if (!string.IsNullOrEmpty(token.EOF))
                    {
                        Console.WriteLine("Read {0} bytes from socket. \n Data : {1}",
                            content.Length, content);
                        Console.WriteLine($"id: {token.Id} / payload: {token.Payload}");

                        Handler.Handle(
                                TaskFamily.TaskFactory.TaskFactory.Create(token.DesingPatternType.Value)
                            );

                        Send(handler, JsonSerializer.Serialize(
                            new SocketToken 
                            {
                                Id = token.Id, 
                                DesingPatternType = token.DesingPatternType, 
                                StatusCode = StatusCode.Ok, 
                                Payload = "server recieced", 
                                EOF = "EOF"
                            }
                        ));
                    }
                    else
                    {
                        handler.Socket.BeginReceive(state.buffer, 0, SocketOption.BufferSize, 0,
                        new AsyncCallback(ReadCallback), state);
                    }
                }
            }
            catch(Exception e)
            {
                threadSignal.Reset();
                throw;
            }
        }
        private static void Send(SocketRecieverSet handler, string data)
        {
            byte[] byteData = Encoding.ASCII.GetBytes(data);

            handler.Socket.BeginSend(byteData, 0, byteData.Length, 0,
                new AsyncCallback(SendCallback), handler);
        }
        private static void SendCallback(IAsyncResult ar)
        {
            try
            {
                var handler = (SocketRecieverSet)ar.AsyncState;
                int bytesSent = handler.Socket.EndSend(ar);
                Console.WriteLine("Sent {0} bytes to client.", bytesSent);

                handler.Socket.Shutdown(SocketShutdown.Both);
                handler.Socket.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
