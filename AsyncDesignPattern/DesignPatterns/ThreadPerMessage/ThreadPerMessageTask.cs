using AsyncDesignPattern.Common.Task;
using System;

namespace ThreadPerMessage
{
    public class ThreadPerMessageTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute ThreadPerMessageTask");
        }
        public static void Main(string[] args)
        {

        }
    }
}
