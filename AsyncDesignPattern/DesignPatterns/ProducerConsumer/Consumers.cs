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
        private Random m_random = new Random();
        private IChannel<string> m_channel;
        public CakeConsumer(IChannel<string> channel)
        {
            m_channel = channel;
        }
        public async void Consume()
        {
            while (true)
            {
                await Task.Delay(m_random.Next(500, 2000));
                var cake = m_channel.Take();
            }
        }
    }
}
