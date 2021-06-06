using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Common
{
    public interface IToken : IHeader, IPayload
    {
    }
}
