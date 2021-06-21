using AsyncDesignPattern.Repository.Repository;
using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Context;
using AsyncDesignPattern.SenderReciever.Context.Builder;
using AsyncDesignPattern.SenderReciever.Reciever;
using AsyncDesignPattern.TaskFamily.Contracts;
using AsyncDesignPattern.TaskFamily.Controller;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ServerWorker>();
                    services.Configure<SocketReciever>(option =>
                    {
                        option.AddHandler(new TaskHandler(new List<ISurveillance> { new MemorySurveillance() }));
                        option.UseContext(new SocketContextBuilder()
                                            .AddAddressFamily(AddressFamily.InterNetwork)
                                            .AddSocketType(SocketType.Stream)
                                            .AddProtocolType(ProtocolType.Tcp)
                                            //.AddIpEndPoint(new IPEndPoint(IPAddress.Parse("192.167.10.102"), 5432))
                                            .AddIpEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5432))
                                            .AddIpSendTimeOut(15)
                                            .AddIpRecieveTimeOut(15)
                                            .Build());
                    });
                    services.AddTransient<ITaskHandler, TaskHandler>()
                            .AddTransient<IRepository, MockRecordRepository>();
                });
    }
}
