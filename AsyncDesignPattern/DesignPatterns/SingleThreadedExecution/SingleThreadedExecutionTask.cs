using AsyncDesignPattern.Common.Task;
using SingleThreadedExecution.Interface;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using AsyncDesignPattern.Common.Gen;
using AsyncDesignPattern.Common.Logger;
using SingleThreadedExecution.Classes;

namespace SingleThreadedExecution
{
    public class SingleThreadedExecutionTask : ITask
    {
        ILogger logger = new AsyncDesignPattern.Common.Logger.ConcoleLogger<SingleThreadedExecutionTask>();
        private IGate _gate { get; set; } = new Gate();
        private IGenerator<string> _generator = new RandomAlphabetGenerator();
        public void ExecuteAsync()
        {
            logger.Logging($"Task Execute SingleThreadedExecutionTask");
            var arg = _generator.Generate();
            UserThread.Create(_gate, arg, arg).Run();
        }
        public static void Main(string[] args)
        {
            new SingleThreadedExecutionTask().ExecuteAsync();

            Console.WriteLine("press any key...");
            Console.ReadKey();
        }
        public SingleThreadedExecutionTask AddGate(IGate gate)
        {
            _gate = gate;
            return this;
        }
    }
}
