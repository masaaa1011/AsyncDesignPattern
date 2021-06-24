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
                    var _config = hostContext.Configuration.GetSection("SocketContext");
                    var config = new
                    {
                        AddressFamily = _config["AddressFamily"],
                        SocketType = _config["SocketType"],
                        ProtocolType = _config["ProtocolType"],
                        IPAddress = Environment.MachineName.Equals(hostContext.Configuration.GetSection("Host").GetChildren().FirstOrDefault().Value) ? _config["LocalIPAddress"] : _config["IPAddress"],
                        Port = _config["Port"],
                        SendTimeOut = _config["SendTimeOut"],
                        RecieveTimeOut = _config["RecieveTimeOut"],
                    };

                    services.AddHostedService<ClientWorker>();
                    services.Configure<SocketSender>(option =>
                    {
                        option.UseContext(new SocketContextBuilder()
                                            .AddAddressFamily(Enum.Parse<AddressFamily>(config.AddressFamily))
                                            .AddSocketType(Enum.Parse<SocketType>(config.SocketType))
                                            .AddProtocolType(Enum.Parse<ProtocolType>(config.ProtocolType))
                                            .AddIpEndPoint(new IPEndPoint(IPAddress.Parse(config.IPAddress), int.Parse(config.Port)))
                                            .AddIpSendTimeOut(int.Parse(config.SendTimeOut))
                                            .AddIpRecieveTimeOut(int.Parse(config.RecieveTimeOut))
                                            .Build());
                    });
                });
    }
}
