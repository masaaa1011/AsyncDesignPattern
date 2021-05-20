using System;
using System.Collections.Generic;
using System.Text;

namespace AsyncDesignPattern.Server.ProccessHub.Agent
{
    public interface IHealthChecker
    {
        bool HealthCheck();
    }
}
