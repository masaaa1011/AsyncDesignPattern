using AsyncDesignPattern.SenderReciever.Common;
using AsyncDesignPattern.SenderReciever.Context;
using AsyncDesignPattern.SenderReciever.Context.Builder;
using AsyncDesignPattern.SenderReciever.Reciever;
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
                    //services.Configure<SocketContext>(option =>
                    //{
                    //    option.AddressFamily = AddressFamily.InterNetwork;
                    //    option.SocketType = SocketType.Stream;
                    //    option.ProtocolType = ProtocolType.Tcp;
                    //    option.IPEndPoint = new IPEndPoint(Dns.GetHostName().FirstOrDefault(), 7777);
                    //    option.SendTimeout = 15;
                    //    option.RecieveTimeout = 15;
                    //});

                    //services.AddTransient<SocketContext>();

                    //services.AddTransient<IReciever<SocketContext, SocketToken>, SocketReciever>();
                    //services.AddSingleton<SocketReciever>();
                    //services.AddScoped<IReciever<SocketContext, SocketToken>, SocketReciever>();

                    services.Configure<SocketReciever>(option =>
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
