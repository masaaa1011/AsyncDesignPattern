using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Common.Gen
{
    public interface IGenerator<T>
    {
        T Generate();
    }

    public class RandomAlphabetGenerator : IGenerator<string>
    {
        private static List<string> Source { get; set; } = new(
            "ABCDEFGHIJKLNMOPQRSTU".Select(s => s.ToString())
            );
        private Random Random { get; set; } = new Random();
        private object _lock = new object();

        public string Generate()
        {
            lock (_lock)
            {
                var res = $"{Source.Where((w, i) => Random.Next(Source.Count).Equals(i)).FirstOrDefault()}_this_is_random_value_created_by_generator";
                return res?? Generate();
            }
        }
    }
}
