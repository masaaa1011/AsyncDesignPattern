using AsyncDesignPattern.Server.ProccessHub.Hub;
using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.Server.ProccessHub.Agent
{
    public interface IAgent: IHealthChecker
    {
        public IProccesshub Hub { get; }
        bool Send();
    }
}
