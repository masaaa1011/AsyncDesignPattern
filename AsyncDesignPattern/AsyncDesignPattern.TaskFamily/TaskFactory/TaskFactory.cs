using ActiveObject;
using AsyncDesignPattern.Common.Enum;
using AsyncDesignPattern.Common.Task;
using Balking;
using Future;
using GuardedSuspention;
using Immutable;
using ProducerConsumer;
using ReadWriteLock;
using SingleThreadedExecution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThreadPerMessage;
using ThreadSpecificStrage;
using TwoPhaseTermination;
using WorkerThread;

namespace AsyncDesignPattern.TaskFamily.TaskFactory
{
    public static class TaskFactory
    {
        public static ITask Create(DesingPatternType type) => type switch
        {
            DesingPatternType.SingleThreadedExecution => new SingleThreadedExecutionTask(),
            DesingPatternType.Immutable => new ImmutableTask(),
            DesingPatternType.GuardedSuspention => new GuardedSuspentionTask(),
            DesingPatternType.Balking => new BalkingTask(),
            DesingPatternType.ProducerConsumer => new ProducerConsumerTask(),
            DesingPatternType.ReadWriteLock => new ReadWriteLockTask(),
            DesingPatternType.ThreadPerMessage => new ThreadPerMessageTask(),
            DesingPatternType.Future => new FutureTask(),
            DesingPatternType.TwoPhaseTermination => new TwoPhaseTerminationTask(),
            DesingPatternType.ThreadSpecificStrage => new ThreadSpecificStrageTask(),
            DesingPatternType.ActiveObject => new ActiveObjectTask(),
            _ => throw new Exception("parameter is not valid type")
        };
    }
}
