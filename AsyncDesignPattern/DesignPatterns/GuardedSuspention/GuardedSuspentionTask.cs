using AsyncDesignPattern.Common.Task;
using GuardedSuspention.Classes;
using System;
using System.Threading.Tasks;

namespace GuardedSuspention
{
    public class GuardedSuspentionTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute GuardedSuspentionTask");
        }
        public static void Main(string[] args)
        {
            var requestQueue = new RequestQueue();

            ITask client = new ClientThread(requestQueue);
            ITask server = new ServerThread(requestQueue);

            Task.Run(() => client.ExecuteAsync());
            Task.Run(() => server.ExecuteAsync());
            
            Console.ReadLine();
        }
    }
}
