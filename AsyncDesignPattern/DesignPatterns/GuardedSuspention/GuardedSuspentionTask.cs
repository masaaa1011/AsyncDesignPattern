using AsyncDesignPattern.Common.Task;
using System;

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

        }
    }
}
