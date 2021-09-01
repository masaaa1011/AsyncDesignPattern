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
            provider.GetServices<IWorker>().ToList().ForEach(
                worker => Task.Run( () => worker.Start() )
            );

            Console.ReadLine();
        }
        public static void StartUp(string[] args)
        {
            var data = new Data(m_fileTitle, string.Empty);
            //var saver = new FileDataSaver(m_storeDirectory);
            var saver = new ConsoleDataSaver(m_storeDirectory);

            m_serviceCollection.AddScoped<IWorker, ManualFileSaveClientWorkers>(provider
                => new ManualFileSaveClientWorkers(m_storeDirectory, data, saver));
            m_serviceCollection.AddScoped<IWorker, AutoSaveServerWorkers>(provider
                => new AutoSaveServerWorkers(m_storeDirectory, data, saver));
        }
    }
}
