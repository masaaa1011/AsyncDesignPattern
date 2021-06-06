using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Reciever
{
    public interface IReciever<T, V> where T : IContext where V : IToken
    {
        public T Context { get; }
        public V Receive(V token);
        public Task<V> ReceiveAsync(V token);
    }
}
