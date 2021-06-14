using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Context;
using AsyncDesignPattern.SenderReciever.Reciever;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Server
{
    public class ServerWorker : BackgroundService
    {
        private readonly ILogger<ServerWorker> _logger;
        private readonly SocketReciever _reciever;

        public ServerWorker(ILogger<ServerWorker> logger, IOptions<SocketReciever> reciever)
        {
            _logger = logger;
            _reciever = reciever.Value;
            //_reciever.UseContext((SocketContext)context.Value);
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
