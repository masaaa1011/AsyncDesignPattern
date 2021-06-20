using AsyncDesignPattern.Common.Task;
using System;

namespace ThreadSpecificStrage
{
    public class ThreadSpecificStrageTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute ThreadSpecificStrageTask");
        }
        public static void Main(string[] args)
        {

        }
    }
}
