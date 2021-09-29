using AsyncDesignPattern.Common.Task;
using System;
using System.Collections.Generic;
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
            var data = new CharData(12, new ReadWriteLock());
            var threads = new List<IThread>
            {
                new ReaderThread(data),
                new ReaderThread(data),
                new ReaderThread(data),
                new ReaderThread(data),
                new ReaderThread(data),
                new ReaderThread(data),
                new WriterThread(data, @"qazwsxedcrfvtgb"),
                new WriterThread(data, @"\[]\^@:/-p;.")
            };

            //Parallel.ForEach(threads, t => t.Run());

            threads.ForEach(f => Task.Run(() => f.Run()));
            Console.ReadLine();
        }
    }
}
