using AsyncDesignPattern.Common.Task;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.TaskHub
{
    internal class TaskHub : ITaskHub
    {
        private static readonly ITaskHub _instance = new TaskHub();
        public static ITaskHub Create() => _instance;
        private TaskHub() 
        {
            ProccessCollection = new Queue<ITask>();
            Start();
        }
        public Queue<ITask> ProccessCollection { get; private set; }
        private async void Start()
        {
            while (true)
            {
                try
                {
                    if(ProccessCollection.Count > 0)
                    {
                        Console.WriteLine($"task execute...");
                        await Task.Run(() =>
                        {
                            ProccessCollection.Dequeue().ExecuteAsync();
                        });
                    }
                    else
                    {
                        Console.WriteLine($"no task was started...");
                    }

                    await Task.Delay(5000);
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public void Stack(ITask task)
        {
            ProccessCollection.Enqueue(task);
        }
    }
}
