using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReadWriteLock
{
    public interface IReadLock
    {
        void ReadLock();
        void ReadUnLock();
    }
    public interface IWriteLock
    {
        void WriteLock();
        void WriteUnLock();
    }
    public interface IReadWriteLock : IReadLock, IWriteLock
    {
    }

    public class ReadWriteLock : IReadWriteLock
    {
        private int m_readingReaders = 0;
        private int m_writingWriters = 0;
        private int m_watingWriters = 0;
        private WhitchPreferReadOrWrite m_preferReadOrWrite = WhitchPreferReadOrWrite.Write;
        private ManualResetEventSlim m_signal = new ManualResetEventSlim();

        private object _lock = new object();


        private object m_readLock = new object();
        private object m_readUnLock = new object();
        private object m_writeLock = new object();
        private object m_writeUnLock = new object();

        /// <summary>
        /// Readロック取得
        /// 待ち条件：
        /// 　　①書き込み途中のスレッドが存在する
        /// 　　②優先モードが書き込み & 書き込み待ちのスレッドが存在する
        /// </summary>
        public void ReadLock()
        {
            lock (_lock)
            {
                try
                {
                    while (m_writingWriters > 0 || (m_preferReadOrWrite == WhitchPreferReadOrWrite.Write && m_watingWriters > 0))
                    {
                        Monitor.Wait(_lock);
                    }
                }
                finally
                {
                    m_readingReaders++;
                }

            }
        }

        /// <summary>
        /// Readロック解除
        /// ①読み込み中のスレッド数を1つ減らす
        /// ②優先モードをwriteにする
        /// </summary>
        public void ReadUnLock()
        {
            lock (_lock)
            {
                m_readingReaders--;
                m_preferReadOrWrite = WhitchPreferReadOrWrite.Write;
                Monitor.PulseAll(_lock);
            }
        }

        /// <summary>
        /// 書き込みロック取得
        /// 待ち条件：
        /// 　①読み込み中のスレッドが存在
        /// 　②書き込み中のスレッドが存在
        /// </summary>
        public void WriteLock()
        {
            lock (_lock)
            {
                m_watingWriters++;
                try
                {
                    while (m_readingReaders > 0 || m_writingWriters > 0)
                    {
                        Monitor.Wait(_lock);
                    }
                }
                finally
                {
                    m_watingWriters--;
                }
                m_writingWriters++;
            }
        }

        /// <summary>
        /// 書き込みロック解放
        /// ①書き込み中のスレッドを1つ減らす
        /// ②優先モードをreadにする
        /// </summary>
        public void WriteUnLock()
        {
            lock (_lock)
            {
                m_writingWriters--;
                m_preferReadOrWrite = WhitchPreferReadOrWrite.Read;
                Monitor.PulseAll(_lock);
            }
        }
    }
    public enum WhitchPreferReadOrWrite
    {
        Read = 1,
        Write = 2,
    }

    public class NotReadWriteLock : IReadWriteLock
    {

        /// <summary>
        /// Readロック取得
        /// 待ち条件：
        /// 　　①書き込み途中のスレッドが存在する
        /// 　　②優先モードが書き込み & 書き込み待ちのスレッドが存在する
        /// </summary>
        public void ReadLock()
        {
        }

        /// <summary>
        /// Readロック解除
        /// ①読み込み中のスレッド数を1つ減らす
        /// ②優先モードをwriteにする
        /// </summary>
        public void ReadUnLock()
        {
        }

        /// <summary>
        /// 書き込みロック取得
        /// ロック条件：
        /// 　①読み込み中のスレッドが存在
        /// 　②書き込み中のスレッドが存在
        /// </summary>
        public void WriteLock()
        {
        }

        /// <summary>
        /// 書き込みロック解放
        /// ①書き込み中のスレッドを1つ減らす
        /// ②優先モードをreadにする
        /// </summary>
        public void WriteUnLock()
        {
        }
    }
}
