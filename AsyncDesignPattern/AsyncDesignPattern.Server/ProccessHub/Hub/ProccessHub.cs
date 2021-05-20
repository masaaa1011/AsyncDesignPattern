using AsyncDesignPattern.Common.Proccess;
using AsyncDesignPattern.Server.ProccessHub.Agent;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.Server.ProccessHub.Hub
{
    public class ProccessHub : IProccesshub
    {
        public IAgent Agent { get; internal set; }
        public Queue<IAsyncProccess> ProccessCollection { get; internal set; }
        public void Accept(IAgent agent)
        {
            if (!Agent.Equals(agent)) return;

            ProccessCollection.Dequeue().ExecuteAsync();
        }
        public void Add(IAsyncProccess proccess) => ProccessCollection.Enqueue(proccess);
    }
}
