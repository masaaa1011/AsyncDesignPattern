using AsyncDesignPattern.Repository.Repository;
using AsyncDesignPattern.SenderReciever.Context.Builder;
using AsyncDesignPattern.SenderReciever.Sender;
using AsyncDesignPattern.TaskFamily.Controller;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Client
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
                    services.AddHostedService<ClientWorker>();
                    services.Configure<SocketSender>(option =>
                    {
                        option.UseContext(new SocketContextBuilder()
                                            .AddAddressFamily(AddressFamily.InterNetwork)
                                            .AddSocketType(SocketType.Stream)
                                            .AddProtocolType(ProtocolType.Tcp)
                                            .AddIpEndPoint(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5432))
                                            .AddIpSendTimeOut(15)
                                            .AddIpRecieveTimeOut(15)
                                            .Build());
                    });
                });
    }
}
