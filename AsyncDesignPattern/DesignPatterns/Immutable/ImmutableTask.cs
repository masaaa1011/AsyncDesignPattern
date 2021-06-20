using AsyncDesignPattern.Common.Task;
using System;

namespace Immutable
{
    public class ImmutableTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute ImmutableTask");
        }
        public static void Main(string[] args)
        {

        }
    }
}
