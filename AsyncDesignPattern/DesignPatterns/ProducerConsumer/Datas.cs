using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public interface IData<TType>
    {
        TType Content { get; }
    }

    public class Cake : IData<string>
    {
        private string m_cake;
        public Cake(string cake)
        {
            m_cake = cake;
        }
        public string Content => $"{m_cake}";
    }
}
