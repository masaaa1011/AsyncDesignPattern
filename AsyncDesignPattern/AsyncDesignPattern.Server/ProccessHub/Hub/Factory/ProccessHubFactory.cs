using AsyncDesignPattern.Common.Proccess;
using AsyncDesignPattern.Server.ProccessHub.Agent.Factory;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.Server.ProccessHub.Hub.Factory
{
    public class ProccessHubFactory
    {
        public static ProccessHub Create() => 
            new ProccessHub() { Agent = AgentFactory.Create(), ProccessCollection = new Queue<IAsyncProccess>() };
    }
}
