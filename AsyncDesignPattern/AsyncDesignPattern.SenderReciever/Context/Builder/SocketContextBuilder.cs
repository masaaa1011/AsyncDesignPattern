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
    public class SocketContextBuilder : IContextBuilder<Socket>
    {
        private AddressFamily _addressFamily = AddressFamily.InterNetwork;
        private SocketType _socketType = SocketType.Stream;
        private ProtocolType _protocolType = ProtocolType.Tcp;
        private IPEndPoint _iPEndPoint = new IPEndPoint(Dns.GetHostAddresses(Dns.GetHostName()).FirstOrDefault(), 7777);

        public Socket Build() 
        {
            var socket = new Socket(_addressFamily, _socketType, _protocolType);
            socket.Bind(_iPEndPoint);

            return socket;
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

        public SocketContextBuilder AddIpEndPoint(IPAddress address, int port)
        {
            _iPEndPoint = new IPEndPoint(address, port);
            return this;
        }

    }
}
