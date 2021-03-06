using AsyncDesignPattern.SenderReciever.Sender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Common.State
{
    public class SocketOption : IStateSet
    {
        public const int BufferSize = 1024;
        public byte[] buffer = new byte[BufferSize];
        public StringBuilder sb = new StringBuilder();
        public IStateSet workSocket = null;
    }
}
