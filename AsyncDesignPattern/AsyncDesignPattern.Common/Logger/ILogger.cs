using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.Common.Logger
{
    public interface ILogger
    {
        void Logging(string message);
        void Logging(string message, Exception e);
        void Logging(Exception e);
    }
}
