using AsyncDesignPattern.Common.Enum;
using AsyncDesignPattern.Repository.Factory;
using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Common.Enum;
using AsyncDesignPattern.SenderReciever.Common.Factory;
using AsyncDesignPattern.SenderReciever.Context;
using AsyncDesignPattern.SenderReciever.Sender;
using AsyncDesignPattern.TaskFamily.Controller;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
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
        private readonly ISender<SocketContext, SocketToken> _sender;
        private readonly ITaskHandler _handler;

        public ClientWorker(ILogger<ClientWorker> logger, IOptions<SocketSender> sender)
        {
            _logger = logger;
            _sender = sender.Value;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    //Console.ReadKey();
                    await _sender.SendAsync(
                        SocketTokenFactory.CreateToken(
                            id: Guid.NewGuid(),
                            desingPatternType: DesingPatternType.SingleThreadedExecution, 
                            statusCode: StatusCode.Ask, 
                            payload: "this is message from client"));
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                catch(Exception e)
                {
                    _logger.LogInformation(e.Message);
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}
