using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadWriteLock
{
    public interface IThread
    {
        void Run();
    }

    /// <summary>
    /// データの読み込みスレッド
    /// </summary>
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
                Console.WriteLine($"{DateTime.Now.ToString("HH:mm:ss.fffffff")} [{System.Threading.Thread.CurrentThread.ManagedThreadId}] - Read >>> {string.Join(string.Empty, readBuf)}");
                if (readBuf.Any(r => readBuf[0] != r)) 
                {
                    Console.WriteLine($"データが破損しちゃってます >>> {string.Join(string.Empty, readBuf)}");
                    throw new Exception();
                }
            }
        }
    }

    /// <summary>
    /// データの書き込みスレッド
    /// </summary>
    public class WriterThread : IThread
    {
        private readonly IData<char[], char> m_data;
        private int m_index = 0;
        private readonly string m_filter;
        private readonly int m_delayUplimit;
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

        public WriterThread(IData<char[], char> data, string filter, int delayUplimit)
        {
            m_data = data;
            m_filter = @filter;
            m_delayUplimit = delayUplimit;
        }
        public void Run()
        {
            while (true)
            {
                var c = Next();
                m_data.Write(c);
                Thread.Sleep(m_random.Next(m_delayUplimit));
            }
        }
    }
}
