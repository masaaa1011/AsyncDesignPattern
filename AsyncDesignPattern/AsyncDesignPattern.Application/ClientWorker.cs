using AsyncDesignPattern.Repository.Factory;
using AsyncDesignPattern.TaskFamily.Controller;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Client
{
    public class ClientWorker : BackgroundService
    {
        private readonly ILogger<ClientWorker> _logger;
        private readonly ITaskHandler _handler;

        public ClientWorker(ILogger<ClientWorker> logger, ITaskHandler handler)
        {
            _logger = logger;
            _handler = handler;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
