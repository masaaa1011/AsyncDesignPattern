using AsyncDesignPattern.Common.Task;
using System;

namespace WorkerThread
{
    public class WorkerThreadTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute WorkerThreadTask");
        }
        public static void Main(string[] args)
        {

        }
    }
}
