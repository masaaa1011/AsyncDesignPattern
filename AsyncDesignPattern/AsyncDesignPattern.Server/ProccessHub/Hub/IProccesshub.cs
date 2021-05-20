using AsyncDesignPattern.Common.Proccess;
using AsyncDesignPattern.Server.ProccessHub.Agent;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.Server.ProccessHub.Hub
{
    public interface IProccesshub
    {
        public IAgent Agent { get; }
        Queue<IAsyncProccess> ProccessCollection { get; }
        public void Accept(IAgent agent);
        public void Add(IAsyncProccess poccess);
    }
}
