using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Sender
{
    public interface ISender<T, V> : IUseContext<T> where T : IContext where V : IToken
    {
        public T Context { get; }
        public V Token { get; }
        public void SendAsync(V token);
    }
}
