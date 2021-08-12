using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Common.Logger
{
    public class ConcoleLogger<T> : ILogger
    {
        public void Logging(string message)
        {
            Console.WriteLine($"{typeof(T)}: {message}");
        }

        public void Logging(string message, Exception e)
        {
            Console.WriteLine($"{typeof(T)}: {message}");
            Console.WriteLine($"{typeof(T)}: {e.Message}{Environment.NewLine}{e.StackTrace}");
        }

        public void Logging(Exception e)
        {
            Console.WriteLine($"{typeof(T)}: {e.Message}");
            Console.WriteLine($"{typeof(T)}: {e.StackTrace}");
        }
    }
}
