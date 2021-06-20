using AsyncDesignPattern.Common.Task;
using System;

namespace Future
{
    public class FutureTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute FutureTask");
        }
        public static void Main(string[] args)
        {

        }
    }
}
