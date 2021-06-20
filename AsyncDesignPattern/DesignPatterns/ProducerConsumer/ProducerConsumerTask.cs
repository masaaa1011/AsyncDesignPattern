using AsyncDesignPattern.Common.Task;
using System;

namespace ProducerConsumer
{
    public class ProducerConsumerTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute ProducerConsumerTask");
        }
        public static void Main(string[] args)
        {

        }
    }
}
