using AsyncDesignPattern.Common.Task;
using System;

namespace Balking
{
    public class BalkingTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute BalkingTask");
        }
        public static void Main(string[] args)
        {

        }
    }
}
