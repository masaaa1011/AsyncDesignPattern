using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.Server.HubService.Agent
{
    public interface IHealthChecker
    {
        bool HealthCheck();
    }
}
