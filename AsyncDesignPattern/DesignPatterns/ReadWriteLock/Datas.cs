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
                
                System.Threading.Thread.Sleep(50);
                return source;
            }
            catch(Exception ex)
            {
                return null;
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
                System.Threading.Thread.Sleep(50);
            }
            catch(Exception ex)
            {

            }
            finally
            {
                m_lock.WriteUnLock();
            }
        }
    }
}
