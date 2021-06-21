using AsyncDesignPattern.TaskFamily.Controller;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Executor
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly TaskHandler _handler;

        public Worker(ILogger<Worker> logger, IOptions<TaskHandler> handler)
        {
            _logger = logger;
            _handler = handler.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                try
                {
                    var task = TaskFamily.TaskFactory.TaskFactory.Create(Common.Enum.DesingPatternType.SingleThreadedExecution);
                    _handler.Handle(task);

                    await Task.Delay(1000, stoppingToken);
                }
                catch (OperationCanceledException)
                {
                    return;
                }
            }
        }
    }
}
