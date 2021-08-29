using AsyncDesignPattern.Common.Task;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Balking
{
    public class BalkingTask : ITask
    {
        private static IServiceCollection m_serviceCollection = new ServiceCollection();
        private static string m_storeDirectory = @"./datas";
        private static string m_fileTitle = "sharedFile.txt";
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute BalkingTask");
        }
        public static void Main(string[] args)
        {
            StartUp(args);
            var provider = m_serviceCollection.BuildServiceProvider();

            Task.Run(() =>
            {
                var worker = provider.GetService<IClientWorker>();
                worker.Start();
            });
            Task.Run(() =>
            {
                var worker = provider.GetService<IServerWorker>();
                worker.Start();
            });

            Console.ReadLine();
        }
        public static void StartUp(string[] args)
        {
            var data = new Data(m_fileTitle, string.Empty);
            var saver = new FileDataSaver(m_storeDirectory);
            m_serviceCollection.AddScoped<IClientWorker, ManualFileSaveClientWorkers>(provider =>
            {
                return new ManualFileSaveClientWorkers(m_storeDirectory, data, saver);
            });
            m_serviceCollection.AddScoped<IServerWorker, PeriodicFileSaveServerWorkers>(provider =>
            {
                return new PeriodicFileSaveServerWorkers(m_storeDirectory, data, saver);
            });
        }
    }

}
