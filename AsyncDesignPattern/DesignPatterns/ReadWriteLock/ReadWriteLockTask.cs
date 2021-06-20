using AsyncDesignPattern.Common.Task;
using System;

namespace ReadWriteLock
{
    public class ReadWriteLockTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute ReadWriteLockTask");
        }
        public static void Main(string[] args)
        {

        }
    }
}
