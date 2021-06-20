using ActiveObject;
using AsyncDesignPattern.Common.Enum;
using AsyncDesignPattern.Common.Task;
using Balking;
using Immutable;
using SingleThreadedExecution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.TaskFamily.TaskFactory
{
    public static class TaskFactory
    {
        public static ITask Create(DesingPatternType type) => type switch
        {
            DesingPatternType.SingleThreadedExecution => new SingleThreadedExecutionTask(),
            DesingPatternType.Immutable => new ImmutableTask(),
            DesingPatternType.ActiveObject => new ActiveObjectTask(),
            DesingPatternType.Balking => new BalkingTask(),
            _ => throw new Exception()
        };
    }
}
