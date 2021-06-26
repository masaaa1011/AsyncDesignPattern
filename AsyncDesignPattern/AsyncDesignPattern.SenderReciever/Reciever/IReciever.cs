using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Reciever
{
    public interface IReciever<TContext, TToken> : IUseContext<TContext> where TContext : IContext where TToken : IToken
    {
        public TContext Context { get; }
        public TToken Token { get; }
        public void ReceiveAsync();
    }
}
