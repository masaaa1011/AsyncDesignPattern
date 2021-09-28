using AsyncDesignPattern.Common.Task;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
            // Channelオブジェクト
            IChannel<string> channel = new DinningTableChannel(8);

            // 5人のProducer
            List<IProducer> producers = new List<IProducer>()
            {
                new CakeProducer(channel, "パティシエ1"),
                new CakeProducer(channel, "パティシエ2"),
                new CakeProducer(channel, "パティシエ3"),
                new CakeProducer(channel, "パティシエ4"),
                new CakeProducer(channel, "パティシエ5"),
            };

            // 3人のConsumer
            List<IConsumer> consumers = new List<IConsumer>()
            {
                new CakeConsumer(channel, "消費者1"),
                new CakeConsumer(channel, "消費者2"),
                new CakeConsumer(channel, "消費者3"),
                new CakeConsumer(channel, "消費者4"),
                //new CakeConsumer(channel, "消費者5"),
            };

            // Producer全員に仕事をさせる
            producers.ForEach(p => Task.Run(() => p.Produce()));
            // Consumer全員に仕事をさせる
            consumers.ForEach(c => Task.Run(() => c.Consume()));

            Console.ReadLine();
        }
    }

    /// <summary>
    /// 標準出力の行情報を連番で返す。
    /// </summary>
    public static class ConsolePositionRepository
    {
        private static object _lock = new object();
        private static int currentTop = Console.GetCursorPosition().Top;
        public static (int Left, int Top) GetNextCursorPosition()
        {
            lock (_lock)
            {
                Console.Write(string.Empty);
                var position = Console.GetCursorPosition();
                position.Top = currentTop;
                currentTop++;
                return position;
            }

        }
    }
}
