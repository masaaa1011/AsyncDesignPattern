using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProducerConsumer
{
    public interface IPuttable<TType>
    {
        public void Put(IData<TType> data);
    }
    public interface ITakable<TType>
    {
        IData<TType> Take();
    }
    public interface IChannel<TType> : IPuttable<TType>, ITakable<TType>
    {

    }

    public class DinningTableChannel : IChannel<string>
    {
        /// <summary>
        /// cakeを乗せるスペース
        /// </summary>
        private readonly IData<string>[] m_cakes;
        /// <summary>
        /// テーブルに置けるケーキの最大数
        /// </summary>
        private readonly int m_maxSize;
        /// <summary>
        /// 現在置いてあるcakeの数
        /// </summary>
        private int m_currentSize = 0;
        /// <summary>
        /// 次にcakeを置きくポジション
        /// </summary>
        private int m_nextPutPosition = 0;
        /// <summary>
        /// 次にケーキを取るポジション
        /// </summary>
        private int m_nextTakePosition = 0;
        /// <summary>
        /// putを待たせる為のイベント同期用クラス
        /// 　　Wait() -> このインスタンスのSet()を呼ばれるまで待機します。
        /// 　　Set() -> このインスタンスのWait()状態を解除します。
        /// </summary>
        private ManualResetEventSlim m_putEventSlim = new ManualResetEventSlim();
        /// <summary>
        /// takeを待たせる為のイベント同期用クラス
        /// 　　Wait() -> このインスタンスのSet()を呼ばれるまで待機します。
        /// 　　Set() -> このインスタンスのWait()状態を解除します。
        /// </summary>
        private ManualResetEventSlim m_takeEventSlim = new ManualResetEventSlim();
        /// <summary>
        /// Put()をSynchronizeにするためのロックオブジェクト
        /// </summary>
        private object p_lock= new object();
        /// <summary>
        /// Take()をSynchronizeにするためのロックオブジェクト
        /// </summary>
        private object t_lock = new object();

        /// <summary>
        /// テーブルに置けるケーキの上限を決めます。
        /// </summary>
        /// <param name="maxSize">テーブルに置けるケーキの上限数</param>
        public DinningTableChannel(int maxSize)
        {
            m_cakes = new IData<string>[maxSize];
            m_maxSize = maxSize;
        }

        /// <summary>
        /// 待ち条件：cakesの最大サイズを超える場合にwait
        /// 解放条件：cakeの中身が減ったら解放
        /// 
        /// ①：もしm_cakesが最大まで埋まっていたら待機します。
        /// ②：m_cakesに渡されたcakeを配置します。
        /// ③：もしTake()が待ち状態であれば再開させます。
        /// </summary>
        /// <param name="data"></param>
        public void Put(IData<string> data)
        {
            lock (p_lock)
            {
                // ①：もしm_cakesが最大まで埋まっていたら待機します。
                while (m_cakes.Length <= m_currentSize)
                {
                    m_putEventSlim.Wait();
                }

                // ケーキにラベルを張ります。
                data.AddLabel(CakeIndexer.NextSerialNo());
                // ②：m_cakesに渡されたcakeを配置します。
                m_cakes[m_nextPutPosition] = data;

                // maxSize3: 0 -> 1 -> 2 -> 0 -> 1 -> 2 ...
                m_nextPutPosition = (m_nextPutPosition + 1) % m_cakes.Length;

                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////
                var outputCurrentSize = m_currentSize + 1;
                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////

                // テーブルに置かれているケーキの数を増やします。
                m_currentSize++;

                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////
                var currentPositon = ConsolePositionRepository.GetNextCursorPosition();
                Console.SetCursorPosition(currentPositon.Left, currentPositon.Top);
                Console.WriteLine($"ケーキが用意できました {data.Content} - テーブル内のケーキ数 {outputCurrentSize} / {m_maxSize}");
                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////

                // ③：もしTake()が待ち状態であれば再開させます。
                m_takeEventSlim.Set();
            }
        }

        /// <summary>
        /// 待ち条件：cakesに何も中身がない場合にwait
        /// 解放条件：cakesに中身が追加された場合にkick
        /// 
        /// ①：もしm_cakesがからであれば待機します。
        /// ②：m_cakesからcakeを1つ取り出します。
        /// ③：もしPut()が待ち状態であれば再開させます。
        /// </summary>
        /// <returns></returns>
        public IData<string> Take()
        {
            lock (t_lock)
            {
                // ①：もしm_cakesがからであれば待機します。
                while (m_currentSize == 0)
                {
                    m_takeEventSlim.Wait();
                }

                // テーブルからケーキを取り出します
                var cake = m_cakes[m_nextTakePosition];

                // ②：m_cakesからcakeを1つ取り出します。
                // maxSize3: 0 -> 1 -> 2 -> 0 -> 1 -> 2 ...
                m_nextTakePosition = (m_nextTakePosition + 1) % m_cakes.Length;

                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////
                var outputCurrentSize = m_currentSize - 1;
                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////
 
                // テーブルに置かれているケーキの数を1つ減らします
                m_currentSize--;

                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////
                var currentPositon = ConsolePositionRepository.GetNextCursorPosition();
                Console.SetCursorPosition(currentPositon.Left, currentPositon.Top);
                Console.WriteLine($"ケーキが購入されました。- {cake.Content} - テーブル内のケーキ数 {outputCurrentSize} / {m_maxSize}");
                ////////////////////// どうでも良いところ　----------------- 標準出力表示用 ----------------- //////////////////////

                // ③：もしPut()が待ち状態であれば再開させます。
                m_putEventSlim.Set();

                return cake;
            }
        }
    }
}
