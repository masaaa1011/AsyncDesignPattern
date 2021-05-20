
using AsyncDesignPattern.Server.ProccessHub.Hub;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.Server.ProccessHub.Agent
{
    public class Agent : IAgent
    {
        public IProccesshub Hub { get; internal set; }

        public bool HealthCheck() => true;

        public bool Send()
        {
            if (!HealthCheck()) return false;

            Hub.Accept(this);
            return true;
        }
    }
}
