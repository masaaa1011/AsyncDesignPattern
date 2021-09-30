using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadWriteLock
{
    public interface IReadable<TType>
    {
        TType Read();
    }
    public interface IWriteable<TType>
    {
        void Write(TType source);
    }
    public interface IData<TReadType, TWriteType> : IReadable<TReadType>, IWriteable<TWriteType>
    {

    }

    public class CharData : IData<char[], char>
    {
        private readonly char[] m_buffer;
        private readonly IReadWriteLock m_lock;
        private readonly object m_outputLockObj = new object();
        private readonly int m_delayUplimit;
        private void Logging(string message)
        {
            lock (m_outputLockObj)
            {
                Console.WriteLine(message);
            }
        }

        public CharData(int size, IReadWriteLock lockOjb, int delayUplimit)
        {
            m_buffer = new char[size];
            m_lock = lockOjb;
            m_delayUplimit = delayUplimit;

            Enumerable.Range(0, size).ToList().ForEach(index => m_buffer[index] = '*');
        }
        public char[] Read()
        {
            m_lock.ReadLock();
            try
            {
                var source = new char[m_buffer.Length];
                Enumerable.Range(0, m_buffer.Length).ToList().ForEach(
                    index => source[index] = m_buffer[index]
                    );

                //Logging($"{DateTime.Now.ToString("HH:mm:ss.fffffff")} [{System.Threading.Thread.CurrentThread.ManagedThreadId}] - Read >>> {string.Join("", m_buffer)}");
                System.Threading.Thread.Sleep(m_delayUplimit);

                return source;
            }
            finally
            {
                m_lock.ReadUnLock();
            }
        }

        public void Write(char source)
        {
            m_lock.WriteLock();
            Logging($"{DateTime.Now.ToString("HH:mm:ss.fffffff")} [{System.Threading.Thread.CurrentThread.ManagedThreadId}] - Write >>> {source}");
            try
            {
                Enumerable.Range(0, m_buffer.Length).ToList().ForEach(
                    index => m_buffer[index] = source
                    );

                System.Threading.Thread.Sleep(m_delayUplimit);
            }
            finally
            {
                m_lock.WriteUnLock();
            }
        }
    }
}
