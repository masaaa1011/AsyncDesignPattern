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
        private ManualResetEventSlim m_lockState = new ManualResetEventSlim(true);

        private object m_readLock = new object();
        private object m_readUnLock = new object();
        private object m_writeLock = new object();
        private object m_writeUnLock = new object();

        private void OutputCondition(string method)
        { 
            //Console.WriteLine($"{method} - 読み込み中スレッド={m_readingReaders},書き込み中スレッド={m_writingWriters},書き込み待ちスレッド={m_watingWriters}");
        }

        /// <summary>
        /// Readロック取得
        /// 待ち条件：
        /// 　　①書き込み途中のスレッドが存在する
        /// 　　②優先モードが書き込み & 書き込み待ちのスレッドが存在する
        /// </summary>
        public void ReadLock()
        {
            lock (m_readLock)
            {
                while (m_writingWriters > 0 || (m_preferReadOrWrite == WhitchPreferReadOrWrite.Write && m_watingWriters > 0))
                {
                    m_lockState.Wait();
                }

                m_readingReaders++;
                OutputCondition("ReadLock");
            }
        }

        /// <summary>
        /// Readロック解除
        /// ①読み込み中のスレッド数を1つ減らす
        /// ②優先モードをwriteにする
        /// </summary>
        public void ReadUnLock()
        {
            lock (m_readUnLock)
            {
                m_readingReaders--;
                m_preferReadOrWrite = WhitchPreferReadOrWrite.Write;

                m_lockState.Set();
                OutputCondition("ReadUnLock");
            }
        }

        /// <summary>
        /// 書き込みロック取得
        /// ロック条件：
        /// 　①読み込み中のスレッドが存在
        /// 　②書き込み中のスレッドが存在
        /// </summary>
        public void WriteLock()
        {
            lock (m_writeLock)
            {
                m_watingWriters++;
                try
                {
                    while (m_readingReaders > 0 || m_writingWriters > 0)
                    {
                        m_lockState.Wait();
                    }
                }
                finally
                {
                    m_watingWriters--;
                }
                m_writingWriters++;
                OutputCondition("WriteLock");
            }
        }

        /// <summary>
        /// 書き込みロック解放
        /// ①書き込み中のスレッドを1つ減らす
        /// ②優先モードをreadにする
        /// </summary>
        public void WriteUnLock()
        {
            lock (m_writeUnLock)
            {
                m_writingWriters--;
                m_preferReadOrWrite = WhitchPreferReadOrWrite.Read;
                m_lockState.Set();
                OutputCondition("WriteUnLock");
            }
        }
    }
    public enum WhitchPreferReadOrWrite
    {
        Read = 1,
        Write = 2,
    }
}
