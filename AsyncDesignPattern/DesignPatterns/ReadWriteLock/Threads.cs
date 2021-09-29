using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteLock
{
    public interface IThread
    {
        void Run();
    }

    public class ReaderThread : IThread
    {
        private readonly IData<char[], char> m_data;
        public ReaderThread(IData<char[], char> data)
        {
            m_data = data;
        }
        public void Run()
        {
            while (true)
            {
                var readBuf = m_data.Read();
            }
        }
    }

    public class WriterThread : IThread
    {
        private readonly IData<char[], char> m_data;
        private int m_index = 0;
        private readonly string m_filter;
        private static readonly Random m_random = new Random();
        private char Next()
        {
            var c = m_filter.ToCharArray()[m_index];
            m_index++;
            if(m_index >= m_filter.Length)
            {
                m_index = 0;
            }

            return c;
        }

        public WriterThread(IData<char[], char> data, string filter)
        {
            m_data = data;
            m_filter = @filter;
        }
        public async void Run()
        {
            while (true)
            {
                var c = Next();
                m_data.Write(c);
                await System.Threading.Tasks.Task.Delay(m_random.Next(3000));
            }
        }
    }
}
