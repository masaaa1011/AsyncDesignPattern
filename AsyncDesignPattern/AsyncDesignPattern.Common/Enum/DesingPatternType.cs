using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Common.Enum
{
    public enum DesingPatternType
    {
        None = 0,
        SingleThreadedExecution = 1,
        Immutable = 2,
        GuardedSuspention = 3,
        Balking = 4,
        ProducerConsumer = 5,
        ReadWriteLock = 6,
        ThreadPerMessage = 7,
        WorkerThread= 8,
        Future = 9,
        TwoPhaseTermination = 10,
        ThreadSpecificStrage = 11,
        ActiveObject = 12
    }
}
