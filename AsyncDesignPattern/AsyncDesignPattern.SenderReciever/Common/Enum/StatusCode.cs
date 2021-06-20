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
        Ask = 1,
        Success = 2,
        Completed = 3,
        Error = 4,
        Wait = 5,
        TimeOut = 6
    }
}
