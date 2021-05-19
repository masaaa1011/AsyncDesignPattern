using AsyncDesignPattern.Server.HubService.Hub;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.Server.HubService.Agent
{
    public interface IAgent: IHealthChecker
    {
        public IProccesshub Hub { get; }
        bool Send();
    }
}
