using AsyncDesignPattern.Common.Task;
using System;

namespace ActiveObject
{
    public class ActiveObjectTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute ActiveObjectTask");
        }
    }
}
