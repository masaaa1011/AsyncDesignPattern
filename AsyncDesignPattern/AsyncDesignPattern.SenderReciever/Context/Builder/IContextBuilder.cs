using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsyncDesignPattern.SenderReciever.Context.Builder
{
    public interface IContextBuilder<T>
    {
        T Build();
    }
}
