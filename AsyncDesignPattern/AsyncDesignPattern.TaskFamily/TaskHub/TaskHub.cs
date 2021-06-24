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
            TaskCollection = new Queue<ITask>();
            Task.Run(() => { Start(); });
        }
        public Queue<ITask> TaskCollection { get; private set; }
        public async void Start()
        {
            while (true)
            {
                try
                {
                    if(TaskCollection.Count > 0)
                    {
                        Console.WriteLine($"task execute...");
                        await Task.Run(() =>
                        {
                            TaskCollection.Dequeue().ExecuteAsync();
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
            TaskCollection.Enqueue(task);
        }
    }
}
