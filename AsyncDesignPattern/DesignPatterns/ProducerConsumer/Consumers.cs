using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public interface IConsumer
    {
        void Consume();
    }

    public class CakeConsumer : IConsumer
    {
        private readonly string m_consumername;
        private readonly Random m_random = new Random();
        private readonly IChannel<string> m_channel;
        public CakeConsumer(IChannel<string> channel, string consumername)
        {
            m_channel = channel;
            m_consumername = consumername;
        }
        public async void Consume()
        {
            while (true)
            {
                await Task.Delay(m_random.Next(1000, 3000));

                var cake = m_channel.Take();

                var output = string.Empty;

                // ----------------- 標準出力表示用 -----------------
                var currentCursorPosition = ConsolePositionRepository.GetNextCursorPosition();
                // ----------------- 標準出力表示用 -----------------
                new List<string>(cake.Content.Select(s => s.ToString())).ForEach(f =>
                {
                // ----------------- 標準出力表示用 -----------------
                    output += f;
                    Console.SetCursorPosition(currentCursorPosition.Left, currentCursorPosition.Top);
                    Console.WriteLine($"{m_consumername}(消費者)がケーキを食べています。({output})"); 
                // ----------------- 標準出力表示用 -----------------

                    Thread.Sleep(m_random.Next(10, 100));
                });
            }
        }
    }
}
