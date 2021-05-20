using AsyncDesignPattern.Common.Proccess;
using SingleThreadedExecution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.DesignPatternProccessRepository.Repository
{
    public class SingleThreadedExecutionProccessRepository : IProccessRepository
    {
        public IAsyncProccess CreateSingleProccess() => new SingleThreadedExecutionProccess();
    }
}
