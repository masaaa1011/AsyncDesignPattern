using AsyncDesignPattern.Common.Proccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Server.Controller
{
    public interface IProccessController
    {
        void Request(IAsyncProccess proccess);
    }
}
