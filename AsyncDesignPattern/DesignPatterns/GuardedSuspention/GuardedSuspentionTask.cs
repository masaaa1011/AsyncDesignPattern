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

            var Client = new ClientThread(requestQueue);
            var server = new ServerThread(requestQueue);

            Task.Run(() => Client.Run());
            Task.Run(() => server.Run());
            
            Console.ReadLine();
        }
    }
}
