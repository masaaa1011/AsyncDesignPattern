using AsyncDesignPattern.Common.Task;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ReadWriteLock
{
    public class ReadWriteLockTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute ReadWriteLockTask");
        }
        public static void Main(string[] args)
        {
            //var data = new CharData(size: 12, lockOjb: new ReadWriteLock(), delayUplimit: 1000);
            var data = new CharData(size: 12, lockOjb: new NotReadWriteLock(), delayUplimit: 1000);
            var threads = new List<IThread>
            {
                new ReaderThread(data: data),
                new ReaderThread(data: data),
                new ReaderThread(data: data),
                new ReaderThread(data: data),
                new ReaderThread(data: data),
                new ReaderThread(data: data),
                new WriterThread(data: data, filter: @"qazwsxedcrfvtgb", delayUplimit: 3000),
                new WriterThread(data: data, filter: @"\[]\^@:/-p;.", delayUplimit: 3000)
            };

            Parallel.ForEach(threads, t => t.Run());
        }
    }
}
