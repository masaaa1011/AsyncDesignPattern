using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkingConsoleApplication
{
    /// <summary>
    /// C# のTaskAwaiterの実装ではTaskのインスタンスをAwaiterが受け取り、AwaiterがTaskの制御をします
    /// ステートマシンとしての役割を持ちます
    /// 
    /// ここではawait asyncをするためのシンプルな実装の為Taskを使わずに制御も何もしません
    /// 
    /// Awaiterとasync/awaitについて
    /// https://ufcpp.net/study/csharp/sp5_awaitable.html
    /// </summary>
    public struct MyAwaiter : INotifyCompletion
    {
        /// <summary>
        /// 処理が完了したかを表す
        /// </summary>
        public bool IsCompleted { get => false; }
        /// <summary>
        /// GetResult()を呼びだす
        /// </summary>
        /// <param name="continuation"></param>
        void INotifyCompletion.OnCompleted(Action continuation)
        {
            continuation(); // こいつが呼ばれるとGetResultが呼ばれる
        }

        /// <summary>
        /// 呼び出し元へと戻り値を返す
        /// 前回の勉強会で誰か何で戻り値がTaskなの？？って言ってた人いた記憶なんだけど、
        /// 実際にはこのGetResultを通じてやり取りをしているからTaskが戻り値になるんやな、、、ってとこ
        /// 逆にいうと自分でTaskクラスとAwaiterクラスを実装した場合、そのTaskの代替えクラスが戻り値になる
        /// </summary>
        /// <returns></returns>
        public object GetResult()
            => new object();
    }
    public class MyAwaitable
    {
        /// <summary>
        /// Awaiterの実装として以下の3つが必要
        /// ① IsCompleted
        /// ② OnCompleted
        /// ③ GetResult
        /// 
        /// またこれらはダックタイピングでの実装であればOk
        /// IEnumerableとIEnumratorも同じようにダックタイピングの実装があればOkとなっている
        /// https://qiita.com/shimgo/items/9d9fbab1e3a7c4343f7b
        /// </summary>
        private MyAwaiter _awaiter = new MyAwaiter();
        /// <summary>
        /// var response = await 〇〇
        /// の記述にて GetAwaiter の実装が必要となる
        /// また、await された際に動くのはこのメソッド
        /// </summary>
        /// <returns></returns>
        public MyAwaiter GetAwaiter()
            => _awaiter;
    }
    class Program
    {
        //public static async Task Main(string[] args)
        //{
        //    var awaitable = new MyAwaitable();
        //    var response = await awaitable; // -> ここで30秒かかる
        //    Console.WriteLine(response);
        //}
    }
}
