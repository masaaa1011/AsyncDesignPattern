using AsyncDesignPattern.Repository.Entities;
using AsyncDesignPattern.Repository.Repository;
using AsyncDesignPattern.Repository.Repository.Components.Concrete.Mock;
using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Context;
using AsyncDesignPattern.SenderReciever.Reciever;
using AsyncDesignPattern.TaskFamily.Controller;
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
        private readonly IReciever<SocketContext, SocketToken> _reciever;
        private readonly MockRecordRepository<MockEntity> _repository;

        public ServerWorker(ILogger<ServerWorker> logger, IOptions<SocketReciever> reciever, IOptions<MockRecordRepository<MockEntity>> repository, IServiceProvider provider)
        {
            _logger = logger;
            _reciever = reciever.Value;
            _repository = repository.Value;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _reciever.ReceiveAsync();
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                catch (Exception e)
                {
                    _logger.LogInformation(e.Message);
                }
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
