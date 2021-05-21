using AsyncDesignPattern.Repository.Dto;
using AsyncDesignPattern.Repository.Entities;
using AsyncDesignPattern.Repository.Repository;
using AsyncDesignPattern.Server.Controller;
using AsyncDesignPattern.Server.Service;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client
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
                    services.AddHostedService<Worker>()
                            .AddTransient<IAsyncService, AsyncProccessService>()
                            .AddTransient<IProccessController, ProccessController>()
                            .AddTransient<IRepository, MockRecordRepository<MockEntity, MockRecord>>();
                });
    }
}
