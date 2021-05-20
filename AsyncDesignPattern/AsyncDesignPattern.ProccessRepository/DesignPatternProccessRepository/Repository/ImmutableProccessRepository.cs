using AsyncDesignPattern.Common.Proccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.DesignPatternProccessRepository.Repository
{
    public class ImmutableProccessRepository : IProccessRepository
    {
        public IAsyncProccess CreateSingleProccess()
        {
            throw new NotImplementedException();
        }
    }
}
