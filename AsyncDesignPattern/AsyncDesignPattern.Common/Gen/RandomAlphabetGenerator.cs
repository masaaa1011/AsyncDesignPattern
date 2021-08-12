using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Common.Gen
{
    public static class RandomAlphabetGenerator
    {
        private static List<string> Source { get; set; } = new(
            "ABCDEFGHIJKLNMOPQRSTU".Select(s => s.ToString())
            );
        private static Random Random { get; set; } = new Random();
        private static object _lock = new object();

        public static string Generate() 
        {
            lock (_lock)
            {
                var res = Source.Where((w, i) => Random.Next(Source.Count).Equals(i)).FirstOrDefault();
                return res?? Generate();
            }
        }
    }
}
