using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.Server.HubService.Agent.Factory
{
    public static class AgentFactory
    {
        public static IAgent Create() 
            => new Agent();
    }
}
