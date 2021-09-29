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
        private void Logging(string message)
        {
            lock (m_outputLockObj)
            {
                Console.WriteLine(message);
            }
        }

        public CharData(int size, IReadWriteLock lockOjb)
        {
            m_buffer = new char[size];
            m_lock = lockOjb;

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

                Logging($"[{System.Threading.Thread.CurrentThread.ManagedThreadId}] - Read: {string.Join("", m_buffer)}");
                System.Threading.Thread.Sleep(1000);
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
            try
            {

                Enumerable.Range(0, m_buffer.Length).ToList().ForEach(
                    index => m_buffer[index] = source
                    );

                Logging($"[{System.Threading.Thread.CurrentThread.ManagedThreadId}] - Write: {source}");
                System.Threading.Thread.Sleep(1000);
            }
            finally
            {
                m_lock.WriteUnLock();
            }
        }
    }
}
