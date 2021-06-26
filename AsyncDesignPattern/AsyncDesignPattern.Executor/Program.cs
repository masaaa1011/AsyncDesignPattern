using AsyncDesignPattern.Repository.Entities;
using AsyncDesignPattern.Repository.Repository;
using AsyncDesignPattern.TaskFamily.Contracts;
using AsyncDesignPattern.TaskFamily.Controller;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Executor
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
                            .AddTransient<IRepository, MockRecordRepository<MockEntity>>();

                    services.Configure<TaskHandler>(option =>
                    {
                        option.AddSurveillance(new MemorySurveillance());
                    });
                });
    }
}
