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
        /// <summary>
        /// Consumer名
        /// </summary>
        private readonly string m_consumername;
        /// <summary>
        /// Channelオブジェクト
        /// </summary>
        private readonly IChannel<string> m_channel;
        /// <summary>
        /// ケーキを食べる時間のランダム時間用
        /// </summary>
        private readonly Random m_random = new Random();
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

                // テーブルからケーキを取り出します。
                var cake = m_channel.Take();
                
                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////
                var output = string.Empty;
                var currentCursorPosition = ConsolePositionRepository.GetNextCursorPosition();
                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////

                new List<string>(cake.Content.Select(s => s.ToString())).ForEach(f =>
                {
                    ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////
                    output += f;
                    Console.SetCursorPosition(currentCursorPosition.Left, currentCursorPosition.Top);
                    Console.WriteLine($"{m_consumername}(消費者)がケーキを食べています。({output})");
                    ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////

                    Thread.Sleep(m_random.Next(10, 100));
                });
            }
        }
    }
}
