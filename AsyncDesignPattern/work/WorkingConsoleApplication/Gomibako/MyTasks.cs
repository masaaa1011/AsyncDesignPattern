using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WorkingConsoleApplication
{
    public class MyTaskProject
    {
        public static async Task Execute(string[] args)
        {
            StartUp();
            await Start(args);
        }

        /// <summary>
        /// ConsoleApplicationの仕様状メインスレッドのコンテキストを設定しないとダメなので設定してます。
        /// </summary>
        public static void StartUp()
        {
            var sc = new SynchronizationContext();
            SynchronizationContext.SetSynchronizationContext(sc);
        }
        public static async Task Start(string[] args)
        {
            var result = await MyTask<string>.Run(() =>
            {
                return "this is response message from lambda";
            });
            
            Console.WriteLine(result);
        }
    }

    /// <summary>
    /// await をするためのstruct
    /// MicrosoftのTaskAwaiterクラスの構成を真似ています
    /// https://referencesource.microsoft.com/#mscorlib/system/runtime/compilerservices/TaskAwaiter.cs,2c48fb3bdfc69022
    /// <typeparam name="TResult"></typeparam>
    public struct MyTaskAwaiter<TResult> : INotifyCompletion
    {
        /// <summary>
        /// 実処理を行うMyTaskオブジェクト
        /// </summary>
        private readonly MyTask<TResult> m_task;
        /// <summary>
        /// MyTaskオブジェクト
        /// </summary>
        /// <param name="task"></param>
        internal MyTaskAwaiter(MyTask<TResult> task)
            => m_task = task;
        /// <summary>
        /// 実処理を行う関数
        /// ※メインスレッドから呼び出しをされないと動作しません。ここは手抜きです。
        /// </summary>
        /// <param name="task"></param>
        /// <param name="continuation"></param>
        internal static void OnCompletedInternal(MyTask<TResult> task, Action continuation)
        {
            var mainThread = SynchronizationContext.Current;
            task.SetContinuationForAwait(() =>
            {
                mainThread.Post(_ => continuation(), null);
            });
        }
        /// <summary>
        /// 処理が完了した場合にtrueを返す
        /// </summary>
        public bool IsCompleted
            => m_task.IsCompleted;
        void INotifyCompletion.OnCompleted(Action continuation)
            => OnCompletedInternal(m_task, continuation);
        /// <summary>
        /// awaitをする為に必要な関数
        /// ダックタイピングでの実装
        /// </summary>
        /// <returns></returns>
        public TResult GetResult()
            => m_task.m_result;
    }

    /// <summary>
    /// Taskとして実処理を担当するクラス
    /// MicrosoftのTaskクラスの構成を真似ています
    /// https://referencesource.microsoft.com/#mscorlib/system/threading/Tasks/Future.cs,5ca7b77f3df89fc6
    /// </summary>
    /// <typeparam name="TResult"></typeparam>
    public class MyTask<TResult> // : Task(C# の実装上はGenerictは拡張ポイントとして実装)
    {
        /// <summary>
        /// Taskとして渡されたlambdaの結果を返す為の変数
        /// </summary>
        internal TResult m_result;
        /// <summary>
        /// 状態管理変数
        /// </summary>
        internal TaskState m_state = TaskState.Started;
        /// <summary>
        /// MyTask.Runにてlambdaで渡された関数
        /// </summary>
        internal Func<TResult> m_action;
        /// <summary>
        /// AwaterからOnComplete()を通じて呼び出されるcallback用の関数
        /// </summary>
        internal Action m_callback;
        /// <summary>
        /// コンストラクタでlambdaを受け取ります。
        /// </summary>
        /// <param name="function"></param>
        internal MyTask(Func<TResult> function)
            => m_action = function;
        /// <summary>
        /// OnComplete時のcallback関数(GetResult())を取り扱います。
        /// </summary>
        /// <param name="continuationAction"></param>
        internal void SetContinuationForAwait(Action continuationAction)
        {
            m_callback = continuationAction;
            if (IsCompleted && m_callback != null)
                m_callback();
        }
        /// <summary>
        /// Task.Run<TResult>()の真似です。
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public static MyTask<TResult> Run(Func<TResult> function)
        {
            var t = new MyTask<TResult>(function);
            t.Start(function);
            return t;
        }
        /// <summary>
        /// var response = await {Instance}と書いた際にこの関数が最初に呼ばれます
        /// </summary>
        /// <returns></returns>
        public MyTaskAwaiter<TResult> GetAwaiter()
            => new MyTaskAwaiter<TResult>(this);
        public bool IsCompleted
            => m_state == TaskState.Completed;
        /// <summary>
        /// lambdaで渡された関数を実行します。
        /// </summary>
        /// <param name="function"></param>
        public void Start(Func<TResult> function)
        {
            ThreadPool.QueueUserWorkItem(_ =>
            {
                m_result = m_action();
                m_state = TaskState.Completed;
                if (m_callback != null)
                    m_callback();
            });
        }
    }
    enum TaskState
    {
        Started = 1,
        Invoked = 2,
        Completed = 3,
        Faulted = 4,
    }
}
