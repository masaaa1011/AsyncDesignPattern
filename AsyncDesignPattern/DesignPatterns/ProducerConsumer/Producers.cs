using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public interface IProducer
    {
        void Produce();
    }

    public class CakeProducer : IProducer
    {
        private Random m_random = new Random();
        private IChannel<string> m_channel;
        private object _lock = new object();
        public CakeProducer(IChannel<string> channel)
        {
            m_channel = channel;
        }
        public async void Produce()
        {
            while (true)
            {
                await Task.Delay(m_random.Next(0, 5000));
                
                // 注意：ここでの番号振りは同期性を持たないため連番例は持ちません。(サンプルコードですと)
                var cake = new Cake($"LabelNo: {CakeIndexer.NextSerialNo()} / {CakeRepository.本日の気まぐれケーキ()}");
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
        public static int NextSerialNo()
        {
            lock (_lock)
            {
                m_serialNo++;
                return m_serialNo;
            }
        }
    }
}
