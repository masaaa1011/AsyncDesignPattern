
using AsyncDesignPattern.Server.HubService.Hub;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.Server.HubService.Agent
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
