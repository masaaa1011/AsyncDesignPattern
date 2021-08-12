using AsyncDesignPattern.Common.Task;
using Immutable.Classes;
using System;
using System.Threading.Tasks;

namespace Immutable
{
    public class ImmutableTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute ImmutableTask");
        }
        public static void Main(string[] args)
        {
            var alice = new Person("Alice", "America");

            Task.Run(() => new PrintPersonThread(alice).Run());
            Task.Run(() => new PrintPersonThread(alice).Run());
            Task.Run(() => new PrintPersonThread(alice).Run());

            Console.ReadKey();
        }
    }
}
