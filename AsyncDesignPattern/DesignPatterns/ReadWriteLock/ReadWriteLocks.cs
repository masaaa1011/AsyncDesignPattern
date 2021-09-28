using System;
using System.Collections.Generic;
using System.Linq;
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
        private WhitchPreferReadOrWrite m_preferWriter = WhitchPreferReadOrWrite.Write;
        private ManualResetEventSlim m_lockState = new ManualResetEventSlim();

        private void OutputCondition()
            => Console.WriteLine($"読み込み中スレッド={m_readingReaders},書き込み中スレッド={m_writingWriters},書き込み待ちスレッド={m_watingWriters}");

        /// <summary>
        /// Readロック取得
        /// 待ち条件：
        /// 　　①書き込み途中のスレッドが存在する
        /// 　　②優先モードが書き込み & 書き込み待ちのスレッドが存在する
        /// </summary>
        public void ReadLock()
        {
            while (m_writingWriters > 0 || (m_preferWriter == WhitchPreferReadOrWrite.Write && m_watingWriters > 0))
            {
                m_lockState.Wait();
            }
            m_readingReaders++;
        }

        /// <summary>
        /// Readロック解除
        /// ①読み込み中のスレッド数を1つ減らす
        /// ②優先モードをwriteにする
        /// </summary>
        public void ReadUnLock()
        {
            m_readingReaders--;
            m_preferWriter = WhitchPreferReadOrWrite.Write;

            m_lockState.Set();
        }

        /// <summary>
        /// 書き込みロック取得
        /// ロック条件：
        /// 　①読み込み中のスレッドが存在
        /// 　②書き込み中のスレッドが存在
        /// </summary>
        public void WriteLock()
        {
            m_watingWriters++;
            while (m_readingReaders > 0 || m_writingWriters > 0)
            {
                m_lockState.Wait();
            }
            m_watingWriters--;
            m_writingWriters++;
        }

        /// <summary>
        /// 書き込みロック解放
        /// ①書き込み中のスレッドを1つ減らす
        /// ②優先モードをreadにする
        /// </summary>
        public void WriteUnLock()
        {
            m_writingWriters--;
            m_preferWriter = WhitchPreferReadOrWrite.Read;
            m_lockState.Set();
        }
    }
    public enum WhitchPreferReadOrWrite
    {
        Read = 1,
        Write = 2,
    }
}
