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
        void AddLabel(string label);
    }

    public class Cake : IData<string>
    {
        private string m_cake;
        public Cake(string cake)
        {
            m_cake = cake;
        }
        public string Content => $"{m_cake}";
        public void AddLabel(string label)
        {
            m_cake = $"label: {label} - {m_cake}";
        }

    }
}
