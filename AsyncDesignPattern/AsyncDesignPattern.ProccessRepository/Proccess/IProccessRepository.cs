using AsyncDesignPattern.Common.Proccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Repository.Proccess
{
    public interface IProccessRepository
    {
        List<IAsyncProccess> CreateMock();
    }
}
