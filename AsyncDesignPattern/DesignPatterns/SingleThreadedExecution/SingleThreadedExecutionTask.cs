using AsyncDesignPattern.Common.Task;
using System;

namespace SingleThreadedExecution
{
    public class SingleThreadedExecutionTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute SingleThreadedExecutionTask");
        }
    }
}
