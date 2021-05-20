using AsyncDesignPattern.Common.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.DesignPatternProccessRepository.Repository.Factory
{
    public static class ProccessRepositoryFactory
    {
        public static IProccessRepository Create(DesingPatternType type) => type switch
        {
            DesingPatternType.SingleThreadedExecution => new SingleThreadedExecutionProccessRepository(),
            DesingPatternType.Immutable => new ImmutableProccessRepository(),
            DesingPatternType.ActiveObject => new ActiveObjectProccessRepository(),
            _ => throw new Exception("予期せぬrepository type")
        };
    }
}
