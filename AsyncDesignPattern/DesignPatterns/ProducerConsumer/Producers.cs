using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public interface IProducer
    {
        void Produce();
    }

    public class CakeProducer : IProducer
    {
        /// <summary>
        /// 生産者の名前
        /// </summary>
        private readonly string m_producerName;
        /// <summary>
        /// Channelオブジェクト
        /// </summary>
        private readonly IChannel<string> m_channel;
        /// <summary>
        /// ケーキを作る時間のランダム時間用
        /// </summary>
        private readonly Random m_random = new Random();
        public CakeProducer(IChannel<string> channel, string producerName)
        {
            m_channel = channel;
            m_producerName = producerName;
        }
        public async void Produce()
        {
            while (true)
            {
                await Task.Delay(m_random.Next(1000, 3000));

                // ケーキを作ります。
                var cake = new Cake(CakeRepository.本日の気まぐれケーキ());

                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////
                var output = string.Empty;
                var currentCursorPosition = ConsolePositionRepository.GetNextCursorPosition();
                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////

                new List<string>(cake.Content.Select(s => s.ToString())).ForEach(f => 
                {
                    ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////
                    output += f;
                    Console.SetCursorPosition(currentCursorPosition.Left, currentCursorPosition.Top);
                    Console.WriteLine($"{m_producerName}(生産者)がケーキ作っています。({output})");
                    ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////

                    Thread.Sleep(m_random.Next(50, 300));
                });

                // ケーキをテーブルに置きます。
                m_channel.Put(cake);
            }
        }
    }

    public static class CakeRepository
    {
        private static List<string> m_cakes = new List<string> 
        {
            "チョコレートケーキ",
            "モンブラン",
            "シフォンケーキ",
            "メロンケーキ",
            "ショートケーキ",
        };
        public static string 本日の気まぐれケーキ()
        {
            return m_cakes.OrderBy(o => Guid.NewGuid()).FirstOrDefault();
        }
    }

    public static class CakeIndexer
    {
        private static int m_serialNo;
        private static object _lock = new object();
        public static string NextSerialNo()
        {
            lock (_lock)
            {
                m_serialNo++;
                return m_serialNo.ToString();
            }
        }
    }
}
