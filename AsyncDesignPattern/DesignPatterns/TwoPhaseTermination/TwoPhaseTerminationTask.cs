using AsyncDesignPattern.Common.Task;
using System;

namespace TwoPhaseTermination
{
    public class TwoPhaseTerminationTask : ITask
    {
        public void ExecuteAsync()
        {
            Console.WriteLine($"Task Execute TwoPhaseTerminationTask");
        }
        public static void Main(string[] args)
        {

        }
    }
}
