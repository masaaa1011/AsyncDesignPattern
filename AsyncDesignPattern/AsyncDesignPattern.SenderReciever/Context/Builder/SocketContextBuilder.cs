using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Context.Builder
{
    public class SocketContextBuilder : IContextBuilder<SocketContext>
    {
        private AddressFamily _addressFamily { get; set; } = AddressFamily.InterNetwork;
        private SocketType _socketType = SocketType.Stream;
        private ProtocolType _protocolType = ProtocolType.Tcp;
        private IPEndPoint _iPEndPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(), 7777);
        private int _sendTimeout = 15;
        public int _recieveTimeout = 15;

        public SocketContext Build() 
        {
            return new SocketContext()
            {
                AddressFamily = _addressFamily,
                SocketType = _socketType,
                ProtocolType = _protocolType,
                IPEndPoint = _iPEndPoint,
                SendTimeout = _sendTimeout,
                RecieveTimeout = _recieveTimeout
            };
        }

        public SocketContextBuilder AddAddressFamily(AddressFamily addressFamily)
        {
            _addressFamily = addressFamily;
            return this;
        }
        public SocketContextBuilder AddSocketType(SocketType socketType)
        {
            _socketType = socketType;
            return this;
        }
        public SocketContextBuilder AddProtocolType(ProtocolType protocolType)
        {
            _protocolType = protocolType;
            return this;
        }

        public SocketContextBuilder AddIpEndPoint(IPEndPoint endPoint)
        {
            _iPEndPoint = endPoint;
            return this;
        }

        public SocketContextBuilder AddIpSendTimeOut(int timeout)
        {
            _sendTimeout = timeout;
            return this;
        }
        public SocketContextBuilder AddIpRecieveTimeOut(int timeout)
        {
            _recieveTimeout = timeout;
            return this;
        }

    }
}
