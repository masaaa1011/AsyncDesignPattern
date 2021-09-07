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
            IChannel<string> channel = new DinningTableChannel(8);

            // 5人のProducer
            List<IProducer> producers = new List<IProducer>()
            {
                new CakeProducer(channel),
                new CakeProducer(channel),
                new CakeProducer(channel),
                new CakeProducer(channel),
                new CakeProducer(channel),
            };

            // 3人のConsumer
            List<IConsumer> consumers = new List<IConsumer>()
            {
                new CakeConsumer(channel),
                new CakeConsumer(channel),
                new CakeConsumer(channel),
            };

            producers.ForEach(p => Task.Run(() => p.Produce()));
            consumers.ForEach(c => Task.Run(() => c.Consume()));

            Console.ReadLine();
        }
    }
}
