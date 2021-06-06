using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Common.Enum
{
    public enum StatusCode
    {
        None = 0,
        Success = 1,
        Error = 2,
        Wait = 3,
        TimeOut = 4
    }
}
